using Elsa.Scheduling.Activities;
using Elsa.Workflows.Core;
using Elsa.Workflows.Core.Activities;
using Elsa.Workflows.Core.Contracts;
using Elsa.Workflows.Core.Memory;
using Elsa.Workflows.Core.Models;

namespace Elsa.Samples.AspNet.Issue4440.Workflows;

public class ForEachWorkflow: WorkflowBase
{
    protected override void Build(IWorkflowBuilder builder)
    {
        var currentValueVariable = new Variable<string>();
        var shoppingList = new[] { "Apples", "Bananas", "Potatoes", "Coffee", "Honey", "Rice" };

        builder.WithVariable(currentValueVariable);
        builder.Root = new Sequence
        {
            Variables = { currentValueVariable },
            Activities =
            {
                new WriteLine("Going through the shopping list..."),
                new ForEach<string>(shoppingList)
                {
                    CurrentValue = new Output<string>(currentValueVariable),
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