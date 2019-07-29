using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
namespace BL
{
    /// <summary>
    /// factory and singleton to BL
    /// </summary>
    public static class FactoryBL
    {
        private static IBL bl;
        public static IBL Instance()
        {
            return bl ?? (bl = new MyBL());
        }
    }

    public interface IBL
    {
        IEnumerable<Test> ListTestIdTrainee(int mID);
        IEnumerable<Test> ListTestIdTester(int mID);

        void DeleteAllTrainee();
        void DeleteAllTester();
        void DeleteAllTest();

        /// <summary>
        ///  The function check if the tester is old enough and send the tester to the function in the DAL layer
        /// </summary>
        /// <param name="t">is the tester we want to add</param>
        void AddTester(Tester t);
        /// <summary>
        /// The function cherck if the trainee that going examine: 1.made enough lessons 
        /// 2. old enough 3. didn't made test in the last week befure
        /// The function also check if the tester who going to test this test: 
        /// 1. test on the same type of car that the trainee will test about
        /// 2.He still has not reached the maximum weekly test
        /// 3.did not have another test in the same hour and send the test to the function in DAL layer
        /// if the tester is not suitable the functio will offer all the Suitable and available testers to this test
        /// </summary>
        /// <param name="t">is the test we want to add </param>
        void AddTest(Test t);
        /// <summary>
        /// The function check if the trainee is old enough and send him to the function in the DAL layer
        /// </summary>
        /// <param name="t">The trainee we want to add </param>
        void AddTrainee(Trainee t);

        /// <summary>
        ///   The function send the id of the tester that we want to delete
        /// </summary>
        /// <param name="id">The id of the tester</param>
        void DeleteTester(int id);
        /// <summary>
        /// The fonction send the id of the trainee that
        /// we want to delete to the function in th DAL layer
        /// </summary>
        /// <param name="id">The id of the trainee</param>
        void DeleteTrainee(int id);

        /// <summary>
        /// The function send a tester with the details we want
        /// to update to the function in the DAL layer
        /// </summary>
        /// <param name="t">the tester with the updated details</param>
        void UpdateTester(Tester t);
        /// <summary>
        /// the function send a trainee with the details we want
        /// to update to the function in the DAL layer
        /// </summary>
        /// <param name="t">the trainee with the updated details</param>
        void UpdateTrainee(Trainee t);
        /// <summary>
        /// the function check if the 'answer' we want to update is Suitable for
        /// the criterions and send the criterion, the answer and the not of the
        /// tester to the function in the DAL layer
        /// </summary>
        /// <param name="numtest">the number of the test</param>
        /// <param name="c"> the criterions we want to update</param>
        /// <param name="a">the answer that we want to update to the test</param>
        /// <param name="tnote">the note that the tester wrote aboutethe test</param>
        void UpdateTest(int numtest, Criterion c, Answer a, string tnote);

        /// <summary>
        /// The function bring the list from the function in the DAL layer
        /// </summary>
        /// <returns>the list</returns>
        List<Tester> GetTestersList();
        /// <summary>
        /// The function bring the list from the function in the DAL layer
        /// </summary>
        /// <returns>the list</returns>
        List<Test> GetTestsList();
        /// <summary>
        /// The function bring the list from the function in the DAL layer
        /// </summary>
        /// <returns>the list</returns>
        List<Trainee> GetTraineesList();

        /// <summary>
        /// return true or false if the thes success
        /// </summary>
        /// <param name="test">the test that we check</param>
        /// <returns></returns>
        bool SuccessTest(Test test);
        /// <summary>
        /// the function that get an address and returns all testers who live within X miles of the address
        /// </summary>
        /// <param name="address">the address that we cin</param>
        /// <returns></returns>
        void CalculateDistance(object obj);
        /// <summary>
        /// the function that get a date and time and returns all available testers at that time.the function
        /// check whether the date and time are the tester's working hours and whether the tester is not taking at the same time.
        /// </summary>
        /// <param name="DayAndHour">the day and hour that we cin</param>
        /// <returns></returns>
        IEnumerable<Tester> AvailableTesters(Day DayAndHour);
        /// <summary>
        ///the function that can return all tests that fit a specific condition
        /// </summary>
        /// <param name="Del">our delegate</param>
        /// <returns></returns>
        IEnumerable<Test> BoolTests(Predicate<Test> Del);
        /// <summary>
        /// the function that get a student , and returns the number of tests that he did
        /// </summary>
        /// <param name="trainee">that trainee that we check</param>
        /// <returns></returns>
        int TraineeTestNum(Trainee trainee);
        /// <summary>
        /// the function that get a student and returns if he is entitled to a driving license(if he passed a test driving).
        /// </summary>
        /// <param name="trainee">that trainee that we check</param>
        /// <returns></returns>
        bool SuccessTrainee(Trainee trainee);
        /// <summary>
        /// the function return IEnumerable of all tests scheduled by day/month
        /// </summary>
        /// <param name="time">the time that we cin</param>
        /// <returns></returns>
        IEnumerable<Test> TestOnDate(DateTime time);

        /// <summary>
        /// the function return IEnumerable (IGrouping) of all trainees by school and first name
        /// </summary>
        /// <param name="ToSort">a bool parameter to sort the list by school and first name </param>
        /// <returns></returns>
        List<Trainee> GroupBySchoolTrainee(bool ToSort = false);
        /// <summary>
        /// the function return IEnumerable (IGrouping) of all testers by CarType and first name
        /// </summary>
        /// <param name="ToSort">a bool parameter to sort the list by CarType and first name </param>
        /// <returns></returns>
        List<Tester> GroupByCarTypeTester(bool ToSort = false);
        /// <summary>
        /// the function return IEnumerable (IGrouping) of all trainees by Teacher and first name
        /// </summary>
        /// <param name="ToSort">a bool parameter to sort the list by Teacher and first name </param>
        /// <returns></returns>
        List<Trainee> GroupByTeacherOfTrainee(bool ToSort = false);
        /// <summary>
        /// the function return IEnumerable (IGrouping) of all trainees by NumTest and first name
        /// </summary>
        /// <param name="ToSort">a bool parameter to sort the list by NumTest and first name </param>
        /// <returns></returns>
        List<Trainee> GroupByNumTestTrainee(bool ToSort = false);

        /// <summary>
        /// the function print list/IEnumerable of testers
        /// </summary>
        /// <param name="list">the list/IEnumerable that we want to print</param>
        void PrintTesterList(IEnumerable<Tester> list);
        /// <summary>
        /// the function print list/IEnumerable of trainees 
        /// <param name="list">the list/IEnumerable that we want to print</param>
        void PrintTraineeList(IEnumerable<Trainee> list);
        /// <summary>
        /// the function print list/IEnumerable of tests
        /// <param name="list">the list/IEnumerable that we want to print</param>
        string PrintTestList(IEnumerable<Test> list);

        /// <summary>
        /// the function search in the datasource the trainee by id
        /// </summary>
        /// <param name="mID">the id that we cin to find the trainee</param>
        /// <returns></returns>
        Trainee FindTraineeByID(int mID);
        /// <summary>
        /// the function search in the datasource the tester by id
        /// </summary>
        /// <param name="mID">the id that we cin to find the tester</param>
        /// <returns></returns>
        Tester FindTesterByID(int mID);
        /// <summary>
        /// the function search in the datasource the test by numtest
        /// </summary>
        /// <param name="num">the numtest that we cin to find the test</param>
        /// <returns></returns>
        Test FindTestBuyNum(int num);
        /// <summary>
        /// the function return true/false if this test in January
        /// </summary>
        /// <param name="t">the test that we check</param>
        /// <returns></returns>
        bool JanuaryTest(Test t);
        /// <summary>
        /// the function return true/false if this test in November
        /// </summary>
        /// <param name="t">the test that we check</param>
        /// <returns></returns>
        bool NovemberTest(Test t);
        /// <summary>
        /// the function return true/false if this test in December
        /// </summary>
        /// <param name="t">the test that we check</param>
        /// <returns></returns>
        bool DecemberTest(Test t);
        bool FailTest(Test t);
    }
}