using System.Collections.Generic;

namespace Excersice1.BL
{
    public class User1
    {
        int Id;
        string Name;
        string Email;
        string Password;
        bool IsAdmin;
        bool IsActive;

        static List<Course> myCourses = new List<Course>();
        static List<User1> usersList = new List<User1>();

        static User1()
        {
            // Initialize the admin user
            usersList.Add(new User1(1, "admin", "admin@admin.com", "admin", true, true));
        }

        public User1() { }

        public User1(int id, string name, string email, string password, bool isAdmin, bool isActive)
        {
            Id1 = id;
            Name1 = name;
            Email1 = email;
            Password1 = password;
            IsAdmin1 = isAdmin;
            IsActive1 = isActive;
        }

        public bool Insert()
        {
            foreach (User1 user in usersList)
            {
                if (user.Id == this.Id)
                {
                    return false;
                }
            }
            usersList.Add(this);
            return true;
        }

        public static bool addMyCourse(Course course1)
        {
            foreach (Course course in myCourses)
            {
                if (course.Id == course1.Id || course.Title1 == course1.Title1)
                {
                    return false;
                }
            }
            myCourses.Add(course1);
            return true;
        }

        public List<Course> GetByDurationRange(double duration1, double duration2)
        {
            List<Course> courses = new List<Course>();
            foreach (Course course in myCourses)
            {
                if (course.Duration1 >= duration1 && course.Duration1 <= duration2)
                {
                    courses.Add(course);
                }
            }
            return courses;
        }

        public List<Course> GetByRatingRange(double ratingFrom, double ratingTo)
        {
            List<Course> courses = new List<Course>();
            foreach (Course course in myCourses)
            {
                if (course.Rating1 >= ratingFrom && course.Rating1 <= ratingTo)
                {
                    courses.Add(course);
                }
            }
            return courses;
        }

        public static void DeleteCourse(int id)
        {
            bool removed = false;
            foreach (Course course in myCourses)
            {
                if (course.Id == id)
                {
                    myCourses.Remove(course);
                    removed = true;
                    break;
                }
            }
            if (!removed)
            {
                throw new ArgumentException("Course with ID: " + id + " not found!");
            }
        }

        public static List<User1> Read()
        {
            return usersList;
        }

        public static List<Course> GetCourses()
        {
            return myCourses;
        }

        public static User1 Login(string email, string password)
        {
            foreach (User1 user in usersList)
            {
                if (user.Email1 == email && user.Password1 == password)
                {
                    user.IsActive = true;
                    return user;
                }
            }
            return null; // Login failed
        }

        public static void Logout(User1 user1)
        {
            user1.IsActive = false;
        }

        public bool Register()
        {
            this.IsAdmin1 = false; // Default to false
            this.IsActive1 = true; // Default to true

            foreach (User1 user in usersList)
            {
                if (user.Email1 == this.Email1) // Ensure unique email
                {
                    return false;
                }
            }
            usersList.Add(this);
            return true;
        }

        public int Id1 { get => Id; set => Id = value; }
        public string Name1 { get => Name; set => Name = value; }
        public string Email1 { get => Email; set => Email = value; }
        public string Password1 { get => Password; set => Password = value; }
        public bool IsAdmin1 { get => IsAdmin; set => IsAdmin = value; }
        public bool IsActive1 { get => IsActive; set => IsActive = value; }
    }
}
