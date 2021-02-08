using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class TGraphView : GraphView
{
    public TGraphView()
    {
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        this.SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        GridBackground bg = new GridBackground();
        this.Insert(0, bg);
        bg.StretchToParentSize();

        //styleSheets.Add(Resources.Load<StyleSheet>("TGraphGrid"));

        TGraphNode entryNode = this.GenerateEntryNode();
    }

    private TGraphNode GenerateEntryNode()
    {
        TGraphNode node = new TGraphNode();
        node.title = "Entry";
        node.GUID = System.Guid.NewGuid().ToString();
        node.NodeTitle = "EntryPoint";
        node.IsEntryNode = true;
        node.SetPosition(new Rect(0, 0, 200, 100));

        Port port = node.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
        port.portName = "Next";
        node.outputContainer.Add(port);

        node.RefreshExpandedState();
        node.RefreshPorts();
        this.AddElement(node);
        return node;
    }

    public TGraphNode CreateGraphNode(string aNodeName)
    {
        TGraphNode node = new TGraphNode();
        node.title = aNodeName;
        node.GUID = System.Guid.NewGuid().ToString();
        node.NodeTitle = aNodeName;
        node.SetPosition(new Rect(100, 100, 200, 100));

        Port inPort = node.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
        node.outputContainer.Add(inPort);

        Button btnAddOutPort = new Button(() => {
            Port outPort = node.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
            node.outputContainer.Add(outPort);
            node.RefreshExpandedState();
            node.RefreshPorts();
        });
        btnAddOutPort.text = "Add OutPort";
        node.titleContainer.Add(btnAddOutPort);

        node.RefreshExpandedState();
        node.RefreshPorts();

        this.AddElement(node);
        return node;
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        List<Port> compatiblePorts = new List<Port>();
        this.ports.ForEach((Port port) =>
        {
            if (startPort.direction == port.direction)
            {
                return;
            }
            if (port != startPort && port.node != startPort.node)
            {
                compatiblePorts.Add(port);
            }
        });
        return compatiblePorts;
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        if (evt.target == this)
        {
            evt.menu.AppendAction("Add TGraphNode", (e)=> {
                this.CreateGraphNode("TGraphNode");
            });
        }
        base.BuildContextualMenu(evt);
    }
}
