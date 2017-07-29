﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Models.Sections.Section_1
{
    using Academy.Models.Contracts;
    using Academy.Models.Utils.Output;
    using static Utils.Validation.CurriculumValidator;

    internal class Course : ICourse
    {
        private string name;
        private int lecturesPerWeek;

        public Course(string name, string lecturesPerWeek, string startingDate)
        {
            this.Name = name;
            this.LecturesPerWeek = int.Parse(lecturesPerWeek);
            this.StartingDate = DateTime.Parse(startingDate);
            this.EndingDate = this.StartingDate.AddDays(30);
        }

        public string Name
        {
            get => this.name;
            set => this.name = CoursenameValidation(value);
        }

        public int LecturesPerWeek
        {
            get => this.lecturesPerWeek;
            set => this.lecturesPerWeek = LecturesPerWeekValidation(value);
        }

        public DateTime StartingDate { get; set; }

        public DateTime EndingDate { get; set; }

        public IList<IStudent> OnsiteStudents { get; } = new List<IStudent>();

        public IList<IStudent> OnlineStudents { get; } = new List<IStudent>();

        public IList<ILecture> Lectures { get; } = new List<ILecture>();

        public override string ToString()
        {
            return StringFormatOutput.CourseOutput(this);
        }
    }
}
