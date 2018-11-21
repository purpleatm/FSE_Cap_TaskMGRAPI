using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TaskManager.DataAccess.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void AddUpdateTaskTestMethod()
        {
            try
            {
                var model = new Model.TaskModel()
                {
                    
                };
                DataAccess.AddUpdateTask(model);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        /// <summary>
        /// AddUpdateParentTaskTestMethod
        /// </summary>
        [Test]
        public void AddParentTaskTestMethod()
        {
            try
            {
                Guid guid = Guid.NewGuid();
                var parentTask = new ParentTask()
                {
                    Parent_ID= guid,
                    Parent_Task = "TestParentTask1",
                    Tasks = new List<Task>()
                    {
                        new Task()
                        {
                            Task_ID=Guid.NewGuid(),
                            Task1="TestTask1",
                            Start_Date=DateTime.Now,
                            End_Date=DateTime.Now.AddMonths(2),
                            Priority=1
                        }
                    }
                };
               bool response = DataAccess.AddParentTask(parentTask);
                if (response)
                {
                    
                }
                else
                {
                    Assert.Fail("No Transaction");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// UpdateParentTaskTestMethod
        /// </summary>
        [Test]
        public void UpdateParentTaskTestMethod()
        {
            try
            {
                Guid guid = Guid.Parse("A81675F1-CE37-4AE7-9DA2-17966F602694");
                var parentTask = new ParentTask()
                {
                    Parent_ID = guid,
                    Parent_Task = "TestParentTask1Modifed1",
                    Tasks = new List<Task>()
                    {
                        new Task()
                        {
                            Task_ID=Guid.NewGuid(),
                            Task1="TestTask2",
                            Start_Date=DateTime.Now,
                            End_Date=DateTime.Now.AddMonths(2),
                            Priority=1
                        }
                    }
                };
                bool response = DataAccess.UpdateParentTask(parentTask);
                if (response)
                {

                }
                else
                {
                    Assert.Fail("No Transaction");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

    }
}
