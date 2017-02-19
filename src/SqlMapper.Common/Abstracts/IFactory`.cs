namespace SqlMapper.Common.Abstracts
{
    internal interface IFactory<out TOut>
    {
        TOut CreateInstance();
    }

    internal interface IFactory<in TIn, out TOut>
    {
        TOut CreateInstance(TIn from);
    }


    internal interface IFactory<in TIn1, in TIn2, out TOut>
    {
        TOut CreateInstance(TIn1 from, TIn2 extra);
    }
}