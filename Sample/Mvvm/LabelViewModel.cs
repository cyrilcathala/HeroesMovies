using System;
namespace Sample.Mvvm
{
    public class LabelViewModel
    {
        public string Label { get; private set; }

        public LabelViewModel(string label)
        {
            Label = label;
        }

        public override string ToString()
        {
            return Label;
        }
    }

    public class LabelViewModel<T>: LabelViewModel
    {
        public T Data { get; private set; }

        public LabelViewModel(T data, string label) : base(label)
        {
            Data = data;
        }
    }
}
