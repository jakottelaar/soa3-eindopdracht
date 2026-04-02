namespace AvansDevOps.Domain.Models.Pipeline
{
    public class PipelineComposite : IPipelineComponent
    {
        private readonly List<IPipelineComponent> _components = new();

        public void Add(IPipelineComponent component)
        {
            _components.Add(component);
        }

        public void Remove(IPipelineComponent component)
        {
            _components.Remove(component);
        }

        public void Execute()
        {
            foreach (var component in _components)
            {
                component.Execute();
            }
        }
    }
}