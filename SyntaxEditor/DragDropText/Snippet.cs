using System.Collections.Generic;

namespace DragDropText
{   
    public class Snippet
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public override string ToString()
        {
            return this.Code;
        }

        public static IEnumerable<Snippet> GetSnippets()
        {
            var snippets = new List<Snippet>(); 
            
            snippets.Add(new Snippet() { Name = "svm", Code = @"static void Main(string[] args)
{

}" });
            snippets.Add(new Snippet() { Name = "class", Code = @"class MyClass
{

}" });

            snippets.Add(new Snippet() { Name = "prop", Code = @"public int MyProperty { get; set; }" });

            snippets.Add(new Snippet() { Name = "propfull", Code = @"private int myVar;

public int MyProperty
{
    get { return myVar; }
    set { myVar = value; }
}" });

            snippets.Add(new Snippet()
            {
                Name = "propdp",
                Code = @"public int MyProperty
{
    get { return (int)GetValue(MyPropertyProperty); }
    set { SetValue(MyPropertyProperty, value); }
}

// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
public static readonly DependencyProperty MyPropertyProperty =
    DependencyProperty.Register(""MyProperty"", typeof(int), typeof(ownerclass), new PropertyMetadata(0));"});

        snippets.Add(new Snippet() { Name = "while", Code = @"while (true)
{

}" });

            return snippets;
        }
    }
}
