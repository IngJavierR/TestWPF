using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TestingWPF.Bindings
{
    class Person : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public Person() { }

        public Person(String name, int age)
        {
            this.name = name;
            this.age = age;
        }

        int age;

        public int Age
        {
            get { return age; }
            set 
            {
                if (this.age != value)
                {
                    this.age = value;
                    NotifyChanged("Age");
                }
            }
        }
        string name;

        public string Name
        {
            get { return name; }
            set 
            {
                if (this.name != value)
                {
                    this.name = value;
                    NotifyChanged("Name");
                }
            }
        }
    }
}
