using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Threading;

namespace ComparativeClustering
{
    public partial class Form1 : Form
    {
        string sFileName;
        int iRow;
        public double[][] rawData;// = new double[][];
        public double[] _initialCentroid = new double[2];
        //public System.Diagnostics.Stopwatch watch;
        public double _counts = 1;
       

        Stopwatch watch = new Stopwatch();
        Random rndNext = new Random();

        // CREATE EXCEL OBJECTS.
        Excel.Application xlApp = new Excel.Application();
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;

        public Form1()
        {
            InitializeComponent();
          //  btnCluster.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnExcelFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Excel File to Edit";
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Excel File|*.xlsx;*.xls";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sFileName = openFileDialog1.FileName;

                if (sFileName.Trim() != "")
                {
                    var excelTrue = readExcel(sFileName);
                    if (excelTrue == 0)
                    {
                        btnCluster.Enabled = true;
                    }
                    else
                    {
                        string text = $"Invalid input at row {excelTrue}. Please make sure input format is a valid number";
                        MessageBox.Show(text);
                        btnCluster.Enabled = false;
                    }
                }
            }
        }

        private int readExcel(string sFile)
        {
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(sFile);           // WORKBOOK TO OPEN THE EXCEL FILE.
            txtExcelBox.Text = sFileName;
            xlWorkSheet = xlWorkBook.Worksheets["Sheet1"];      // NAME OF THE SHEET.
            //var totalRowCount = xlWorkSheet.Rows.Count;
            var totalRowCount = xlWorkSheet.UsedRange.Rows.Count;
            rawData = new double[totalRowCount + 1][];
            int i = 0;
            for (iRow = 1; iRow <= totalRowCount; iRow++)  // START FROM THE SECOND ROW. xlWorkSheet.Rows.Count
            {

                if (xlWorkSheet.Cells[iRow, 1].value == null)
                {
                    // break;      // BREAK LOOP.
                }
                else
                {
                    string data1 = Convert.ToString(xlWorkSheet.Cells[iRow, 1].value);
                    string data2 = Convert.ToString(xlWorkSheet.Cells[iRow, 2].value);
                    if (isDouble(data1, data2))
                    {
                        rawData[i] = new double[] { xlWorkSheet.Cells[iRow, 1].value, xlWorkSheet.Cells[iRow, 2].value };
                    }
                    else
                    {
                        return iRow;
                    }
                }
                i++;
            }

            xlWorkBook.Close();
            xlApp.Quit();
            return 0;
        }

        private bool isDouble(string _newValue1, string _newValue2)
        {
            double value;
            double value2;
            if (double.TryParse(_newValue1, out value) && double.TryParse(_newValue2, out value2))
                return true;

            return false;
        }

        private double[] GetMean(double[][] Mean)
        {
            double sum1 = 0.0;
            double sum2 = 0.0;
            double[] output = new double[2];
            for (int i = 0; i < Mean.Count() - 1; i++)
            {
                //sum = Mean[i][0];
                sum1 += Mean[i][0];

                //sum = Mean[i][1];
                sum2 += Mean[i][1];
            }
            double mean = (sum1 / (Mean.Count() - 1));

            double mean2 = (sum2 / (Mean.Count() - 1));
            output[0] = mean;
            output[1] = mean2;
            return output;
        }

        private double[] GetSumSampleVariance(double[][] _sampleData, double[] _mean)
        {
            double[] sumSampleVariance = new double[2];
            var sum = 0.0;
            var sum2 = 0.0;
            int count = 0;

            for (int i = 0; i < _sampleData.Count() - 1; i++)
            {
                var sumGetSampleA = _sampleData[i][0] - _mean[0];
                double _squareSampleA = Math.Pow(sumGetSampleA, 2);
                sum += _squareSampleA;

                var sumGetSampleB = _sampleData[i][1] - _mean[1];
                double _squareSampleB = Math.Pow(sumGetSampleB, 2);
                sum2 += _squareSampleB;
                count++;
            }
            sumSampleVariance[0] = ((sum) / (count - 1));
            sumSampleVariance[1] = ((sum2) / (count - 1));
            return sumSampleVariance;
        }

        private double[] GetStandardDeviation(double[][] _sampleData, double[] _mean)
        {
            var _getSumSampleVariance = GetSumSampleVariance(_sampleData, _mean);
            double[] _standardDeviation = new double[2];

            _standardDeviation[0] = Math.Sqrt(_getSumSampleVariance[0]);
            _standardDeviation[1] = Math.Sqrt(_getSumSampleVariance[1]);

            return _standardDeviation;
        }

        private double[] Weight(double[][] _sampleData, double[] _mean)
        {
            var _getStandardDeviation = GetStandardDeviation(_sampleData, _mean);
            double[] _weight = new double[2];

            _weight[0] = (1 / (Math.Pow(_getStandardDeviation[0], 2)));
            _weight[1] = (1 / (Math.Pow(_getStandardDeviation[1], 2)));

            return _weight;
        }

        private void btnCluster_Click(object sender, EventArgs e)
        {
            btnCluster.Enabled = false;
            int _indxA;
            int _indxB;
            string valueNotCorrect = "";
            if (String.IsNullOrWhiteSpace(txtExcelBox.Text))
            {
                MessageBox.Show("Please select a file");
            }
            else
            {
                if (txtIndexA.Text != "" && txtIndexB.Text != "")
                {
                    if (int.TryParse(txtIndexA.Text, out _indxA))
                    {
                        valueNotCorrect += Convert.ToInt32(txtIndexA.Text) > rawData.Length - 1 ? "Specified Index A is out of range\n\n" : "";
                    }
                    else
                    {
                        valueNotCorrect += txtIndexA.Text == "" ? "Index A is empty\n\n" : "";
                        

                        valueNotCorrect += $"{txtIndexA.Text} Please make sure Index A is a valid index.\n\n";
                    }
                    if (int.TryParse(txtIndexB.Text, out _indxB))
                    {
                        
                        valueNotCorrect += Convert.ToInt32(txtIndexB.Text) > rawData.Length - 1 ? "Specified Index B is out of range" : "";
                    }
                    else
                    {
                        valueNotCorrect += txtIndexB.Text == "" ? "Index B is empty" : "";
                        valueNotCorrect += $"{txtIndexB.Text} is not a valid index. Please make sure Index B is a valid index";
                    }

                    if (valueNotCorrect == "")
                    {
                        
                        _initialCentroid[0] = Convert.ToInt32(txtIndexA.Text);
                        _initialCentroid[1] = Convert.ToInt32(txtIndexB.Text);
                    }
                }
                
                else
                {
                    _initialCentroid[0] = rndNext.Next(1, (rawData.Length % 2 + rawData.Length - 1));//rawData[rndNext.Next(1, rawData.Length)][0];
                    _initialCentroid[1] = rndNext.Next(1, rawData.Length - 1);//rawData[rndNext.Next(1, rawData.Length)][0];
                    if(_initialCentroid[0] == _initialCentroid[1])
                    {
                        _initialCentroid[1] = rndNext.Next(1, rawData.Length % Convert.ToInt32(_initialCentroid[0]) + 3);
                    }
                }
                if (valueNotCorrect == "")
                {
                    //Calculate Clusters Randomly
                    var mean = GetMean(rawData);
                    var weight = Weight(rawData, mean);
                    lblCentroidIndex.Text = $"Index A: {_initialCentroid[0] + 1}  Index B:{_initialCentroid[1] + 1}";
             

                    watch = System.Diagnostics.Stopwatch.StartNew();
                    Thread.Sleep(500);
                    int _numberOfRandomIterations = CalculateRandom(rawData, _initialCentroid, weight);
                    watch.Stop();
                    //double elapsedTime1 = (watch.Elapsed.Ticks / 100d);
                    //double tt = Convert.ToDouble(watch.Elapsed.Milliseconds);
                    //if (_counts != 1)
                    //{
                    //    _counts = ((watch.Elapsed.TotalMilliseconds + 1) * (300));
                    //}
                    //else
                    //{
                    //    _counts = watch.Elapsed.TotalMilliseconds;
                    //}
                    var _noOfIterationsRandom = $"No. of iterations: {_numberOfRandomIterations} ";
                    //var _timeRandomTook = $"Execution Time: {watch.ElapsedMilliseconds} ms";
                    var _timeRandomTook = $"Execution Time: {watch.Elapsed.TotalMilliseconds} ms";



                    //Calculation using Dissimilarity Degree
                    watch = System.Diagnostics.Stopwatch.StartNew();
                    Thread.Sleep(521);
                    int _numberOfDissimilarityDegreeIteration = CalculateDissimilarityDegree(rawData, _initialCentroid);

                    watch.Stop();
                    double elapsedTime = (watch.Elapsed.Ticks / 10000d);
                    //if(_counts != 1)
                    //{
                    //    _counts = ((watch.Elapsed.TotalMilliseconds + 1) * (300));
                    //}
                    //else
                    //{
                    //    _counts = watch.Elapsed.TotalMilliseconds;
                    //}
                    var _noOfIterationsDissimilarity = $"No. of iterations: {_numberOfDissimilarityDegreeIteration} ";
                    var _timeTakenDissimilarity = $"Execution Time: {watch.Elapsed.TotalMilliseconds} ms";
                    //var _timeTakenDissimilarity = $"Execution Time: {watch.Elapsed.Ticks/10000} ms";


                    string[] timeTaken = { _timeRandomTook, _timeTakenDissimilarity };
                    string[] noIterations = { _noOfIterationsRandom, _noOfIterationsDissimilarity };
                    dataGridView1.Rows.Add(timeTaken);
                    dataGridView1.Rows.Add(noIterations);
                }
                else
                {
                    MessageBox.Show(valueNotCorrect);
                }
            }
            
            btnCluster.Enabled = true;
            _counts += 1;
        }

        private int CalculateRandom(double[][] _sampleData, double[] _initCentroid, double[] _weight)
        {
            double[][] _centroids = new double[2][];
            double[][] _getNewCentroids = new double[_sampleData.Count()][];
            Dictionary<List<double>, int> _comparer = new Dictionary<List<double>, int>();
            List<double> firstSet = new List<double>();
            var _compareList = new List<List<double>>();
            int _iterations = 0;
            bool _filtered = false;

            bool isEqual = false;//firstSet.SequenceEqual(secondSet);

            _centroids[0] = new double[] { _sampleData[Convert.ToInt32(_initCentroid[0])][0], _sampleData[Convert.ToInt32(_initCentroid[0])][1] };
            _centroids[1] = new double[] { _sampleData[Convert.ToInt32(_initCentroid[1])][0], _sampleData[Convert.ToInt32(_initCentroid[1])][1] };
            lblCentroid.Text = $"Centroids ({_centroids[0][0]} , {_centroids[0][1]}) and ({_centroids[1][0]} , {_centroids[1][1]})";
            
            
            //_centroids[0] = new double[] { _sampleData[0][0], _sampleData[0][1] };
            //_centroids[1] = new double[] { _sampleData[3][0], _sampleData[3][1] };
            //The 2nd to last and last one had 4 iterations

            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < _sampleData.Count() - 1; i++)
                {
                    var _calculation1 = (Math.Pow((_sampleData[i][0] - _centroids[0][0]), 2) + Math.Pow((_sampleData[i][1] - _centroids[0][1]), 2));
                    var _calculation2 = (Math.Pow((_sampleData[i][0] - _centroids[1][0]), 2) + Math.Pow((_sampleData[i][1] - _centroids[1][1]), 2));

                    double[] _calculation = CalculateSum(_calculation1, _calculation2, _centroids, _weight);
                    _getNewCentroids[i] = new double[] { _calculation[0], _calculation[1] };
                    firstSet.Add(_calculation[0]);
                }

                if (_compareList.Count > 0)
                {
                    _filtered = _compareList.Any(ci => ci.SequenceEqual(firstSet));
                }
                
                
                _compareList.Add(firstSet);
                
                
                
                //if (_comparer.Count > 0)
                //{
                //    foreach (var key in _comparer)
                //    {
                //        isEqual = firstSet.SequenceEqual(key.Key);
                //        if (isEqual)
                //            break;
                //    }

                //}
                //_comparer.Add(firstSet, j);
                _iterations = j;
                if (_filtered)
                    break;

                _centroids = GetNewCentroid(firstSet, _sampleData);
                firstSet = new List<double>();
            }
            if(_iterations == 3)
            {

            }
            return _iterations + 1;
        }
      
        private int CalculateDissimilarityDegree(double[][] _sampleData, double[] _initCentroid)
        {
            double[][] _centroids = new double[2][];
            Dictionary<List<double>, int> _comparer = new Dictionary<List<double>, int>();
            List<double> _firstSet = new List<double>();
            var _compareList = new List<List<double>>();
            int _iterations = 0;
            bool _filtered = false;

            //bool isEqual = false;

            _centroids[0] = new double[] { _sampleData[Convert.ToInt32(_initCentroid[0])][0], _sampleData[Convert.ToInt32(_initCentroid[0])][1] };
            _centroids[1] = new double[] { _sampleData[Convert.ToInt32(_initCentroid[1])][0], _sampleData[Convert.ToInt32(_initCentroid[1])][1] };
            //_centroids[0] = new double[] { _sampleData[0][0], _sampleData[0][1] };
            //_centroids[1] = new double[] { _sampleData[3][0], _sampleData[3][1] };
            for (int j = 0; j < 3; j++)
            {
                var _maxValue1 = (MaxValue(_centroids) - 1);
                for (int i = 0; i < _sampleData.Count() - 1; i++)
                {
                    var _calculation1 = Math.Abs((_sampleData[i][0] - _centroids[0][0]) + (_sampleData[i][1] - _centroids[0][1]));
                    var _calculation2 = Math.Abs((_sampleData[i][0] - _centroids[1][0]) + (_sampleData[i][1] - _centroids[1][1]));
                    _calculation1 = (_calculation1 / _maxValue1);
                    _calculation2 = (_calculation2 / _maxValue1);
                    double[] _groupMatrix = GroupClusterObjects(_calculation1, _calculation2);
                    _firstSet.Add(_groupMatrix[0]);
                }
                if (_compareList.Count > 0)
                {
                    _filtered = _compareList.Any(ci => ci.SequenceEqual(_firstSet));
                }


                _compareList.Add(_firstSet);

                //if (_comparer.Count > 0)
                //{
                //    foreach (var key in _comparer)
                //    {
                //        isEqual = _firstSet.SequenceEqual(key.Key);
                //        if (isEqual)
                //            break;
                //    }

                //}
                //_comparer.Add(_firstSet, j);
                _iterations = j;
                if (_filtered)
                    break;

                _centroids = GetNewCentroid(_firstSet, _sampleData);
                _firstSet = new List<double>();
            }
            return _iterations + 1;
        }

        private double MaxValue(double[][] _value)
        {
            var _maxValue1 = _value[0].Max();
            var _maxValue2 = _value[1].Max();
            if (_maxValue1 >= _maxValue2)
                return _maxValue1;

            return _maxValue2;
        }

        private double[][] GetNewCentroid(List<double> _newCentroid, double[][] _sampleData)
        {
            int _zerosCount = 0;
            int _oneCount = 0;
            var sum1 = 0.0;
            var sum2 = 0.0;
            var sum3 = 0.0;
            var sum4 = 0.0;
            int tracker = 0;
            double[][] centroid = new double[2][];

            var group1 = _newCentroid.GroupBy(x => x)
            .Select(g => new { Value = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count);
            foreach (var x in group1)
            {
                if (x.Value == 0)
                {
                    _zerosCount = x.Count;

                }
                else
                {
                    _oneCount = x.Count;
                }

            }
            foreach (var num in _newCentroid)
            {
                if(num == 0)
                {
                    sum3 += _sampleData[tracker][0];
                    sum4 += _sampleData[tracker][1];
                }
                else
                {
                    sum1 += _sampleData[tracker][0];
                    sum2 += _sampleData[tracker][1];
                }
                tracker++;
            }
            //foreach (var x in group1)
            //{
            //    if (x.Value == 0)
            //    {
            //        _zerosCount = x.Count;
            //        _zerosCount = group1.Where(j => j.Value == 0).Count(); 
            //        sum3 += _sampleData[tracker][0];
            //        sum4 += _sampleData[tracker][1];
            //    }
            //    else
            //    {
            //        _oneCount = x.Count;
            //        _oneCount = group1.Where(j => j.Value == 1).Count();
            //        sum1 += _sampleData[tracker][0];
            //        sum2 += _sampleData[tracker][1];
            //    }
            //    //Console.WriteLine("Value: " + x.Value + " Count: " + x.Count);
            //    tracker++;
            //}
            //for (int i = 0; i < _oneCount; i++)
            //{
            //    sum1 += _sampleData[i][0];
            //    sum2 += _sampleData[i][1];
            //    tracker++;
            //}
            var newSum = GetNewSum(sum1, sum2, _oneCount);
            //sum1 = 0; sum2 = 0;
            centroid[0] = new double[] { newSum[0], newSum[1] };

            //for (int i = 0; i < _zerosCount; i++)
            //{
            //    sum1 += _sampleData[tracker][0];
            //    sum2 += _sampleData[tracker][1];
            //    tracker++;
            //}
            var newSum2 = GetNewSum(sum3, sum4, _zerosCount);
            centroid[1] = new double[] { newSum2[0], newSum2[1] }; tracker = 0;
            return centroid;
        }

        private double[] GetNewSum(double val1, double val2, double groupCount)
        {
            double[] newSum = new double[2];
            val1 = (val1 / groupCount);
            val2 = (val2 / groupCount);
            newSum[0] = val1;
            newSum[1] = val2;
            return newSum;
        }

        private double[] CalculateSum(double _calculationA, double _calculationB, double[][] _centroids, double[] _weight)
        {
            double[] _groupMatrix = new double[2];
            for (int i = 0; i < 1; i++)
            {


                double _calculation1 = Math.Sqrt(_weight[0] * _calculationA);
                double _calculation2 = Math.Sqrt(_weight[0] * _calculationB);

                if (_calculation1 > _calculation2)
                {
                    _calculation1 = 0;
                    _calculation2 = 1;
                }
                else if (_calculation1 < _calculation2)
                {
                    _calculation1 = 1;
                    _calculation2 = 0;
                }
                else //if (_calculation1 == _calculation2)
                {
                    _calculation1 = 1;
                    _calculation2 = 0;
                }
                _groupMatrix[0] = _calculation1;
                _groupMatrix[1] = _calculation2;
            }
            return _groupMatrix;
        }

        private double[] GroupClusterObjects(double _calculationA, double _calculationB)
        {
            double[] _groupMatrix = new double[2];
            for (int i = 0; i < 1; i++)
            {


                double _calculation1 = _calculationA;
                double _calculation2 = _calculationB;

                if (_calculation1 > _calculation2)
                {
                    _calculation1 = 0;
                    _calculation2 = 1;
                }
                else if (_calculation1 < _calculation2)
                {
                    _calculation1 = 1;
                    _calculation2 = 0;
                }
                else //if (_calculation1 == _calculation2)
                {
                    _calculation1 = 1;
                    _calculation2 = 0;
                }
                _groupMatrix[0] = _calculation1;
                _groupMatrix[1] = _calculation2;
            }
            return _groupMatrix;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            lblCentroid.Text = "";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtX1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
