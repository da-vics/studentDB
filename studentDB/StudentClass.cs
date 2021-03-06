﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace studentDB
{
    class StudentClass
    {
        /// <summary>
        /// DataContext Reference
        /// </summary>
        #region PrivateVariables
        private StudentDataClasses1DataContext dataContext;
        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>                                                                                                                                                                                                                                                          
        /// <param name="datcon"></param>

        public StudentClass(StudentDataClasses1DataContext datcon) => this.dataContext = datcon;


        public IEnumerable<string> displayAllStudents()
        {
            try
            {
                var students = from student in dataContext.Students
                               select student.FisrtName + " " + student.LastName;
                return students;
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }


            IEnumerable<string> defaultt;
            defaultt = new List<string>();

            return defaultt;
        }

        public Student showStudentData(string[] str)
        {
            var student = dataContext.Students.FirstOrDefault(st => st.FisrtName == str[0] && st.LastName == str[1]);
            return student;
        }


        public string getUniversity(int id)
        {
            try
            {
                var university = dataContext.Universities.FirstOrDefault(st => st.Id == id);
                return university.UniversityName;
            }


            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return "NULL";

        }


        public string getClass(int? id)
        {
            try
            {
                var classs = dataContext.Classes.FirstOrDefault(st => st.Id == id);
                return classs.ClassName;
            }


            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return "NULL";

        }

        public IEnumerable<string> AllUniversity()
        {
            var unilist = from uni in dataContext.Universities
                          select uni.UniversityName;
            return unilist;
        }


        public IEnumerable<string> AllClass()
        {
            var classlist = from classs in dataContext.Classes
                            select classs.ClassName;
            return classlist;
        }


        public void AddStudent(string uni, string clas, string first_Name, string last_Name)
        {
            try
            {

                StringBuilder fist = new StringBuilder();
                fist.Append(first_Name);
                fist.Replace(first_Name[0], Char.ToUpper(first_Name[0]), 0, 1);

                StringBuilder las = new StringBuilder();
                las.Append(last_Name);
                las.Replace(last_Name[0], Char.ToUpper(last_Name[0]), 0, 1);


                var uniid = dataContext.Universities.FirstOrDefault(st => st.UniversityName == uni);
                int universityId = uniid.Id;

                var classiid = dataContext.Classes.FirstOrDefault(st => st.ClassName == clas);
                int clasId = classiid.Id;

                dataContext.Students.InsertOnSubmit(new Student { FisrtName = fist.ToString(), LastName = las.ToString(), UniversityId = universityId, ClassesId = clasId });
                dataContext.SubmitChanges();
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        public void RemoveStudentFromDB(string stu)
        {
            try
            {
                var spli = stu.Split(' ');

                var students = from student in dataContext.Students
                               where student.FisrtName.Equals(spli[0]) && student.LastName.Equals(spli[1])
                               select student;

                foreach (var un in students)
                {
                    dataContext.Students.DeleteOnSubmit(un);
                }

                dataContext.SubmitChanges();

            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }


    }
}
