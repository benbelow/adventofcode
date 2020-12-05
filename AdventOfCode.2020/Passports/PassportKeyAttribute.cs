using System;

namespace AdventOfCode._2020.Passports
{
    public class PassportKeyAttribute : Attribute
    {
        private string keyName;

        public PassportKeyAttribute(string keyName)
        {
            this.keyName = keyName;
        }
    }
}