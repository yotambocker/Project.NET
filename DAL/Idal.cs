using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public static class FactoryDAL
    {
        private static Idal dal;
        public static Idal Instance()
        {
            return dal ?? (dal = new Dal_XML_imp());
        }
    }

    public interface Idal
    {
        /// <summary>
        /// The function make sure that there is no tester with the same id
        /// in the list of all the testers in the datasource
        /// and add the tester to the list
        /// </summary>
        /// <param name="t">the tester we want to add</param>
        void AddTester(Tester t);
        /// <summary>
        /// The function add a test to the list of all the tests
        /// in the datasource, give a number to the test
        /// and update the number of the tests of the tester of this test
        /// </summary>
        /// <param name="t">the test we want to add</param>
        void AddTest(Test t);
        /// <summary>
        ///The function make sure that there is no trainee with the same id
        /// in the list of all the trainees in the datasource
        /// and add the trainee to the list
        /// </summary>
        /// <param name="t"></param>
        void AddTrainee(Trainee t);
        /// <summary>
        /// the function find the tester with the id it get
        /// in the list in the data-source and delete him
        /// </summary>
        /// <param name="id">the id of the tester</param>
        void DeleteTester(int id);
        /// <summary>
        /// the function find the trainee with the id it get
        /// in the list in the data-source and delete him
        /// </summary>
        /// <param name="id"></param>
        void DeleteTrainee(int id);
        /// <summary>
        /// The function check if there is such tester in the data-source
        /// and replace him with the tester with the updated detailes
        /// </summary>
        /// <param name="t">the tester with the updated detailes</param>
        void UpdateTester(Tester t);
        /// <summary>
        /// The function check if there is such trainee in the data-source
        /// and replace him with the trinee with the updated detailes
        /// </summary>
        /// <param name="t">the trainee with the updated detailes</param>
        void UpdateTrainee(Trainee t);
        /// <summary>
        /// the function find the test with that id test and update it
        /// </summary>
        /// <param name="numtest">the number of the test</param>
        /// <param name="c">the criterions we want to update</param>
        /// <param name="a">the answer we want to update</param>
        /// <param name="tnote">the note that the tester wrote about the test</param>
        void UpdateTest(int numtest, Criterion c, Answer a, string tnote);
        /// <summary>
        /// The function make a copy of the list in the data source and return this
        /// </summary>
        /// <returns>a copy of the list in the data source</returns>
        List<Tester> GetTestersList();
        /// <summary>
        /// The function make a copy of the list in the data source and return this
        /// </summary>
        /// <returns>a copy of the list in the data source</returns>
        List<Test> GetTestsList();
        /// <summary>
        /// The function make a copy of the list in the data source and return this
        /// </summary>
        /// <returns>a copy of the list in the data source</returns>
        List<Trainee> GetTraineesList();
        /// <summary>
        /// The function get an id number of a trainee and return the trainee
        /// with that id number
        /// </summary>
        /// <param name="mID">the id number of the trainee</param>
        /// <returns>the trainee</returns>
        Trainee FindTraineeByID(int mID);
        /// <summary>
        /// The function get an id number of a tester and return the trainee
        /// with that id number
        /// </summary>
        /// <param name="mID">the id number of the tester</param>
        /// <returns>the tester</returns>
        Tester FindTesterByID(int mID);
        /// <summary>
        /// the function return true/false if this test in January
        /// </summary>
        /// <param name="t">the test that we check</param>
        /// <returns></returns>
        Test FindTestByNumtest(int num);

        void DeleteAllTrainee();

        void DeleteAllTester();

        void DeleteAllTest();
    }
}

