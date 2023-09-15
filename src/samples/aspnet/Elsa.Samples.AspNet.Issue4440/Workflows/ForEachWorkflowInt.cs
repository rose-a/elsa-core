using Elsa.Scheduling.Activities;
using Elsa.Workflows.Core;
using Elsa.Workflows.Core.Activities;
using Elsa.Workflows.Core.Contracts;
using Elsa.Workflows.Core.Memory;
using Elsa.Workflows.Core.Models;

namespace Elsa.Samples.AspNet.Issue4440.Workflows;

public class ForEachWorkflowInt: WorkflowBase
{
    protected override void Build(IWorkflowBuilder builder)
    {
        var currentValueVariable = new Variable<int>();
        var list = new[] { 1337, 4711, 815, 42 };

        builder.WithVariable(currentValueVariable);
        builder.Root = new Sequence
        {
            Variables = { currentValueVariable },
            Activities =
            {
                new WriteLine("Going through the int list..."),
                new ForEach<int>(list)
                {
                    CurrentValue = new Output<int>(currentValueVariable),
                    Body = new Sequence
                    {
                        Activities =
                        {
                            new WriteLine(context => $"- [ ] {currentValueVariable.Get(context)}"),
                            new Delay(TimeSpan.FromMilliseconds(250))
                        }
                    }
                },
                new WriteLine("Let's not forget anything!")
            }
        };
    }
}