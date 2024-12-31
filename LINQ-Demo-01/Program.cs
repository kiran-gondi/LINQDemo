namespace LINQ_Demo_01
{
    internal class Program
    {
        public enum Gender
        {
            Male,
            Female,
            Trans
        }

        public class Student
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public int Age { get; set; } = 0;
            public Gender Gender { get; set; }
            public string Class { get; set; }

            public int Score { get; set; }
        }

        

        static void Main(string[] args)
        {
            //LinqEvenNumbers();
            //DelegateDemo();
            //AnonymousDemo();

            List<Student> StudentsList = new List<Student>();
            CreateStudents(StudentsList);
            //DisplayItems<Student>(StudentsList);

            //IEnumerable<string> results = DemoForWhereOrderBySelect(StudentsList);
            //DisplayItems<string>(results);

            //DisplayFemaleStudents(StudentsList);

            //HighScoreStudents(StudentsList);
            //GroupStudentsByClass(StudentsList);

            var maxScore = StudentsList.Max(student => student.Score);
            Console.WriteLine(maxScore);

            var minScroe = StudentsList.Min(student => student.Score);
            Console.WriteLine(minScroe);

            var averageScore = StudentsList.Average(student => student.Score);
            Console.WriteLine(averageScore);

            var totalScoreSum = StudentsList.Sum(student => student.Score);
            Console.WriteLine(totalScoreSum);

            var studentCount = StudentsList.Count(student => student.Age > 18);
            Console.WriteLine(studentCount);

            var highScoreStudents = StudentsList.Count(student => student.Score > 90);
            Console.WriteLine(highScoreStudents);

            //TODO RESUME FROM: Aggregation Operations (Sum, Count, Min, Max, Average)

            Console.ReadKey();
        }

        private static void GroupStudentsByClass(List<Student> StudentsList)
        {
            var studentsByClass = from student in StudentsList
                                  group student by student.Class into studentGroup
                                  select new { Class = studentGroup.Key, Students = studentGroup };

            var studentsByClass1 = StudentsList.GroupBy(x => x.Class)
                                                .Select(studGroup => new
                                                {
                                                    Class = studGroup.Key,
                                                    Students = studGroup
                                                });

            var groupStudentsByAge = from student in StudentsList
                                      group student by student.Age into studentGroupedByAge
                                      select studentGroupedByAge;

            var groupStudentsByAge1 = StudentsList.GroupBy(x => x.Age)
                                        .Select(studGroup => new
                                        {
                                            Age = studGroup.Key,
                                            Students = studGroup
                                        });

            foreach (var studentGroup in studentsByClass1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Students Grouped by Class {studentGroup.Class} is {studentGroup.Students.Count()}");
                Console.ResetColor();
                foreach (var student in studentGroup.Students)
                {
                    Console.WriteLine(student.Id + ", " + student.Name + ", "
                        + student.Age + ", " + student.Gender + ", " + student.Class);
                }
                Console.WriteLine("\n");
            }

            /*foreach (var studentGroup in groupStudentsByAge1) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Students Grouped by Age {studentGroup.Age} is {studentGroup.Students.Count()}");
                Console.ResetColor();
                foreach (var student in studentGroup.Students)
                {
                    Console.WriteLine(student.Id + ", " + student.Name + ", "
                        + student.Age + ", " + student.Gender + ", " + student.Class);
                }
                Console.WriteLine("\n");
            }*/
        }

        private static void HighScoreStudents(List<Student> StudentsList)
        {
            var highScore = from highScoreStudent in StudentsList
                            where highScoreStudent.Score > 80
                            orderby highScoreStudent.Name
                            select highScoreStudent.Name;

            var highScore1 = StudentsList.Where(x => x.Score > 80).OrderByDescending(x => x.Name).Select(x => x.Name);

            DisplayItems<string>(highScore1);
        }

        private static void DisplayFemaleStudents(List<Student> StudentsList)
        {
            var femaleStudents = (from femaleStudent in StudentsList
                                  where femaleStudent.Gender == Gender.Female
                                  orderby femaleStudent.Name ascending
                                  select femaleStudent).Distinct();

            var femaleStudents1 = from femaleStudent in StudentsList
                                  where femaleStudent.Gender == Gender.Female
                                  orderby femaleStudent.Name ascending
                                  select femaleStudent;

            var femaleStud = StudentsList.Where(x => x.Gender == Gender.Female).OrderBy(x => x.Name).Select(x => x.Name);
            DisplayItems<string>(femaleStud);
        }

        private static IEnumerable<string> DemoForWhereOrderBySelect(List<Student> StudentsList)
        {
            return from student in StudentsList
                   where student.Age > 18
                   orderby student.Name ascending
                   select student.Name;

            //var results = StudentsList.Where(x => x.Age > 18).OrderBy(x=> x.Name).Select(x => x.Name);
        }

        private static void CreateStudents(List<Student> StudentsList)
        {
            Student s1 = new Student() { Id = 1, Name = "Ram", Age = 18, Gender = Gender.Male, Class = "8", Score = 60 };
            StudentsList.Add(s1);
            Student s2 = new Student() { Id = 2, Name = "Rahim", Age = 18, Gender = Gender.Male, Class = "8", Score = 90 };
            StudentsList.Add(s2);
            Student s3 = new Student() { Id = 3, Name = "Ramya", Age = 15, Gender = Gender.Female, Class = "6", Score = 82 };
            StudentsList.Add(s3);
            Student s4 = new Student() { Id = 4, Name = "Rachitha", Age = 17, Gender = Gender.Female, Class = "8", Score = 77 };
            StudentsList.Add(s4);
            Student s5 = new Student() { Id = 5, Name = "Asif", Age = 17, Gender = Gender.Male, Class = "6", Score = 92 };
            StudentsList.Add(s5);
            Student s6 = new Student() { Id = 6, Name = "Tom", Age = 37, Gender = Gender.Male, Class = "6", Score = 89 };
            StudentsList.Add(s6);
        }

        public static void AnonymousDemo()
        {
            List<string> names = new List<string> { "Alice", "Bob", "Carol", "David" };

            IEnumerable<string> namesStartingWithC = names.Where(name => name.StartsWith("C"));
            DisplayItems<string>(namesStartingWithC);
        }

        #region Delegate Demo
        public delegate bool IsEvenDelegate(int number);
        public static bool IsEven(int number)
        {
            return number % 2 == 0;
        }
        private static void DelegateDemo()
        {
            IsEvenDelegate isEvenDel = IsEven;
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
            IEnumerable<int> evenNumbers = numbers.Where(n => isEvenDel(n));
            DisplayEvenNumbers(evenNumbers);
        } 
        #endregion

        private static void LinqEvenNumbers()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            IEnumerable<int> evenNumbers;
            bool featureFlag = true;

            if (!featureFlag)
            {
                Console.WriteLine("Even Numbers with LINQ expression");
                evenNumbers = from number in numbers
                              where number % 2 == 0
                              select number;
            }
            else
            {
                Console.WriteLine("Even Numbers with LINQ Lambda expression");
                evenNumbers = numbers.Where(x => x % 2 == 0);

            }

            //DisplayEvenNumbers(evenNumbers);
            DisplayItems<int>(evenNumbers);
        }

        private static void DisplayEvenNumbers(IEnumerable<int> evenNumbers)
        {
            foreach (int number in evenNumbers)
            {
                Console.Write(number + ", ");
            }
        }

        private static void DisplayItems<T>(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                if (item is Student)
                {
                    Student? studentItem = item as Student;
                    Console.WriteLine(studentItem.Id + ", " + studentItem.Name + ", "
                        + studentItem.Age + ", " + studentItem.Gender + ", " + studentItem.Class);
                    Console.WriteLine("\n");
                }
                else
                {
                    Console.Write(item + ", ");
                }
            }
        }

    }
}
