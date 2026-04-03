namespace AvansDevOps.Domain.Models.Pipeline
{
    public class PipelineComposite : IPipelineComponent
    {
        private readonly List<IPipelineComponent> components = [];

        public void Add(IPipelineComponent component)
        {
            components.Add(component);
        }

        public void Remove(IPipelineComponent component)
        {
            components.Remove(component);
        }

        public void Execute()
        {
            foreach (var component in components)
            {
                component.Execute();
            }
        }
    }
}