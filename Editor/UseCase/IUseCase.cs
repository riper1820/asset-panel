namespace RiperBool.AssetPanel.Editor.UseCase
{
    public interface IUseCase<I, O>
    {
        public O Execute(I input);
    }
}