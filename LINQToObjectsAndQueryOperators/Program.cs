using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQToObjectsAndQueryOperators
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UniversityManager um = new UniversityManager();

            um.MaleStudents();
            um.FemaleStudents();
            um.SortStudentsByAge();
            um.AllStudentsFromBejingTech();
            //um.GetAllStudentsByUniversityId();

            int[] someInt = { 30, 12, 4, 3, 12 };
            IEnumerable<int> sortedInts = from i in someInt orderby i select i;
            IEnumerable<int> reversedInts = sortedInts.Reverse();

            foreach(int i in reversedInts)
            {
                Console.WriteLine(i);
            }

            IEnumerable<int> reversedSortedInts = from i in someInt orderby i descending select i;
            foreach (int i in reversedSortedInts)
            {
                Console.WriteLine(i);
            }
            Console.ReadKey();
        }
    }

    class UniversityManager
    {
        public List<University> universities;
        public List<Student> students;

        // constructor
        public UniversityManager()
        {
            universities = new List<University>();
            students = new List<Student>();

            // Let's add some Universities
            universities.Add(new University() { Id = 1, Name = "Yale" });
            universities.Add(new University() { Id = 2, Name = "Beijing Tech" });

            // Let's add some Students
            students.Add(new Student() { Id = 1, Name = "Carla", Gender = "female", Age = 17, UniversityId = 1 });
            students.Add(new Student() { Id = 2, Name = "Toni", Gender = "male", Age = 21, UniversityId = 1 });
            students.Add(new Student() { Id = 3, Name = "Frank", Gender = "male", Age = 20, UniversityId = 1 });
            students.Add(new Student() { Id = 4, Name = "Leyla", Gender = "female", Age = 19, UniversityId = 2 });
            students.Add(new Student() { Id = 5, Name = "James", Gender = "trans-gender", Age = 25, UniversityId = 2 });
            students.Add(new Student() { Id = 6, Name = "Linda", Gender = "female", Age = 22, UniversityId = 2 });
        }

        public void MaleStudents()
        {
            IEnumerable<Student> maleStudents = from student in students where student.Gender == "male" select student;
            Console.WriteLine("Male - Students: ");

            foreach (Student student in maleStudents)
            {
                student.Print();
            }
        }
        public void FemaleStudents()
        {
            IEnumerable<Student> femaleStudents = from student in students where student.Gender == "female" select student;

            Console.WriteLine("Female - Students: ");

            foreach (Student student in femaleStudents)
            {
                student.Print();
            }
        }
        public void SortStudentsByAge()
        {
            var sortedStudents = from student in students orderby student.Age select student;

            Console.WriteLine("Students sorted by Age:");

            foreach (Student student in sortedStudents)
            {
                student.Print();
            }
        }
        public void AllStudentsFromBejingTech()
        {
            IEnumerable<Student> bjtStudents = from student in students
                                               join university in universities on student.UniversityId equals university.Id
                                               where university.Name == "Beijing Tech"
                                               select student;

            Console.WriteLine("Students from Beijing Tech");
            foreach (Student student in bjtStudents)
            {
                student.Print();
            }
        }
        public void GetAllStudentsByUniversityId()
        {
            IEnumerable<Student> studentsByUniversityId;
            Console.WriteLine("Please enter a university id to search students:");
            try
            {
                int universityId = Convert.ToInt32(Console.ReadLine());

                studentsByUniversityId = from student in students
                                                              join university in universities on student.UniversityId equals university.Id
                                                              where university.Id == universityId
                                                              select student;

                if(studentsByUniversityId.Count() != 0)
                {
                    Console.WriteLine("Students from University with ID: " + universityId);
                    foreach (Student student in studentsByUniversityId)
                        student.Print();
                } else
                {
                    Console.WriteLine($"University with id {universityId} not found!");
                    GetAllStudentsByUniversityId();
                }  
            }
            catch (Exception ex)
            {
                Console.WriteLine("Incorrect input!");
                
                GetAllStudentsByUniversityId();
            }
        }
    }
    

    class University
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Print()
        {
            Console.WriteLine("University {0} with id {1}", Name, Id);
        }
    }
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }

        // Foreign Key
        public int UniversityId { get; set; }

        public void Print()
        {
            Console.WriteLine("Student {0}, with Id {1}, Gender {2} and Age {3} from University with Id {4}", Name, Id, Gender, Age, UniversityId);
        }
    }
}
