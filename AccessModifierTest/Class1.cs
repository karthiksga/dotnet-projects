namespace AccessModifierTest
{
    public class Class1
    {
        protected string ProtectedProperty { get; set; } = "TEst";
        protected internal string ProtectedInternalProperty { get; set; } = "Test2";

        public Class1()
        {
            
        }
        protected void ProtectedMethod()
        {
            // This method can only be accessed within this class or by derived classes.
        }
    }

    public class Class2
    {
        public Class2()
        {
            var class1 = new Class1();
            class1.protectedProerty = "Test"; // This will cause a compile-time error because ProtectedProperty is not accessible here.
            class1.ProtectedInternalProperty = "Test3"; // This is valid because Class2 is in the same assembly as Class1.

        }
    }
}
