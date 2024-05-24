using System;
using System.Collections.Generic;

// Terminal node
public partial class DialogueNode : BaseDialogueNode {
    public event EventHandler<List<string>> OnDialogueSent;
    private List<string> messages = new List<string>();

    #region Constructors
    public DialogueNode() { }
    
    public DialogueNode(string message) {
        this.messages.Add(message);
    }

    public DialogueNode(List<string> messages) {
        this.messages = messages;
    }
    #endregion

    public override void Evaluate() {
        OnDialogueSent?.Invoke(this, messages);
    }

    #region Getters and Setters
    public void SetMessages(List<string> messages) { 
        this.messages = messages; 
    }

    public void AddMessage(string message) {
        this.messages.Add(message);
    }
    #endregion

    #region Builder
    public class Builder {
        private readonly List<string> messages = new List<string>();

        public Builder AddMessage(string message) {
            messages.Add(message);
            return this;
        }

        public DialogueNode Build() {
            return new DialogueNode(messages);
        }
    }
    #endregion
}
