using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;
using ViewModel;

namespace Test
{
    [TestClass]
    public class ViewModelBaseTest
    {
        [TestMethod]
        public void ViewModelBaseInterfaceTest()
        {
            ViewModelBaseToTest _toTest = new ViewModelBaseToTest();
            int _PropertyChangedCount = 0;
            string _lastPropertyName = String.Empty;
            _toTest.PropertyChanged += (object sender, PropertyChangedEventArgs e) => { _PropertyChangedCount++; _lastPropertyName = e.PropertyName; };
            _toTest.TestRaisePropertyChanged("Name");
            Assert.AreEqual<string>("Name", _lastPropertyName);
            Assert.AreEqual<int>(1, _PropertyChangedCount);
        }
        private class ViewModelBaseToTest : ViewModelBase
        {
            internal void TestRaisePropertyChanged(string propertyName)
            {
                base.RaisePropertyChanged(propertyName);
            }
        }
    }
}