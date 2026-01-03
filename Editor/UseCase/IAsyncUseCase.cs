using System.Threading.Tasks;

namespace RiperBool.AssetPanel.Editor.UseCase
{
    public interface IAsyncUseCase<I, O>
    {
        public Task<O> Execute(I input);
    }
}