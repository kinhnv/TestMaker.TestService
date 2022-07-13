namespace TestMaker.Common.Attributes
{
    public class EnumNameAttribute : Attribute
    {
        private readonly string _name = string.Empty;

        public EnumNameAttribute(string name)
        {
            _name = name;
        }

        public string Name { get { return _name; } }
    }
}
