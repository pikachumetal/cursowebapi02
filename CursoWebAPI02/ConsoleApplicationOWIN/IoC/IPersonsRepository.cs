using System;
using ConsoleApplicationOWIN.Model;

namespace ConsoleApplicationOWIN.IoC
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPersonsRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Person[] GetAll();
    }

    /// <summary>
    /// 
    /// </summary>
    public class PersonsRepository : IPersonsRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Person[] GetAll()
        {
            return new[] {
                new Person() { Id= 1, Name="Person 1 Self OWIN"},
                new Person() { Id= 2, Name="Person 2 Self OWIN"}
            };
        }
    }
}
