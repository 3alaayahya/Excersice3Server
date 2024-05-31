namespace Excersice1.BL
{
    public class Course
    {
        int id;
        string title;
        string URL;
        double rating;
        int numberOfReviews;
        int instructorsId;
        string imageReference;
        double duration;
        string lastUpdate;
        static List<Course> coursesList = new List<Course>();
        private static readonly SemaphoreSlim listLock = new SemaphoreSlim(1, 1);

        public Course() { }

        public Course(string title, int id, double rating, int numberOfReviews, string lastUpdate, double duration, int instructorsId, string URL, string imageReference)
        {
            Id = id;
            Title1 = title;
            URL1 = URL;
            Rating1 = rating;
            NumberOfReviews1 = numberOfReviews;
            InstructorsId1 = instructorsId;
            ImageReference1 = imageReference;
            Duration1 = duration;
            LastUpdate1 = lastUpdate;
        }

        public bool Update(int id, Course updatedCourse)
        {
            for (int i = 0; i < coursesList.Count; i++)
            {
                if (coursesList[i].id == updatedCourse.id)
                {
                    for (int j = 0; j < coursesList.Count; j++)
                    {
                        if (coursesList[j].Title1 == updatedCourse.Title1 && coursesList[j].Id != updatedCourse.Id)
                        {
                            return false;
                        }
                    }
                        coursesList[i] = updatedCourse;
                        return true;
                }
            }
            return false; // Course not found or invalid title
        }

        public static Course getCourseById(int id)
        {
            foreach (Course course in coursesList)
            {
                if (course.Id == id)
                {
                    return course;
                }
            }
            return null;
        }
        public async Task<bool> Insert()
        {
            await listLock.WaitAsync();
            try
            {
                if (coursesList.Any(course => course.id == this.id || course.Title1 == this.Title1))
                {
                    Console.WriteLine($"Duplicate course found with ID: {this.id} and Title: {this.title}");
                    return false; // Course already exists
                }

                coursesList.Add(this);
                return true; // Course added successfully
            }
            finally
            {
                listLock.Release();
            }
        }

        public static void DeleteById(int id)
        {
            bool removed = false;
            foreach (Course course in coursesList)
            {
                if (course.id == id)
                {
                    coursesList.Remove(course);
                    removed = true;
                    break;
                }
            }
            if (!removed)
            {
                throw new Exception("Course with ID: " + id + " not found!");
            }
        }

        //static public List<Course> Read()
        //{
        //    return coursesList;
        //}
        public static async Task<List<Course>> Read()
        {
            await listLock.WaitAsync();
            try
            {
                return new List<Course>(coursesList); // Return a copy to avoid modification during enumeration
            }
            finally
            {
                listLock.Release();
            }
        }

        public int Id { get => id; set => id = value; }
        public string Title1 { get => title; set => title = value; }
        public string URL1 { get => URL; set => URL = value; }
        public double Rating1 { get => rating; set => rating = value; }
        public int NumberOfReviews1 { get => numberOfReviews; set => numberOfReviews = value; }
        public int InstructorsId1 { get => instructorsId; set => instructorsId = value; }
        public double Duration1 { get => duration; set => duration = value; }
        public string LastUpdate1 { get => lastUpdate; set => lastUpdate = value; }
        public string ImageReference1
        {
            get => imageReference;
            set => imageReference = string.IsNullOrEmpty(value) ? "https://assets-global.website-files.com/64be2485b703f9575bd09a67/64f54aac6ef461a95e69a166_OG.png" : value;
        }

    }
}
