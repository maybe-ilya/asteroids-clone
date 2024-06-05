namespace MIG.API
{
    public interface IFactory<out TOutput>
    {
        TOutput Create();
    }

    public interface IFactory<out TOutput, in TInput>
    {
        TOutput Create(TInput input);
    }

    public interface IFactory<out TOuput, in TInput1, in TInput2>
    {
        TOuput Create(TInput1 input1, TInput2 input2);
    }

    public interface IFactory<out TOuput, in TInput1, in TInput2, in TInput3>
    {
        TOuput Create(TInput1 input1, TInput2 input2, TInput3 input3);
    }
}