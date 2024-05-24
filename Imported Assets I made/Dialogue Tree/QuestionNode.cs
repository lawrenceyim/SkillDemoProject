using System;
using System.Collections.Generic;

// Terminal node
public partial class QuestionNode : BaseDialogueNode
{
    public event EventHandler<(string, List<string>)> OnQuestionSent;

    private string question;
    private List<string> responses = new List<string>();
    private List<Action> responseActions = new List<Action>();

    public QuestionNode() { }
    public QuestionNode(string question, List<string> responses, List<Action> responseActions) {
        this.question = question;
        this.responses = responses;
        this.responseActions = responseActions;
    }

    public override void Evaluate() {
        OnQuestionSent?.Invoke(this, (question, responses));
    }

    public void ReceivePlayerResponse(int index) {
        responseActions[index]();
    }

    #region Getters and Setters
    public void SetQuestion(string question) {
        this.question = question;
    }

    public void SetResponses(List<string> responses) {
        this.responses = responses;
    }

    public void SetResponseActions(List<Action> responseActions) {
        this.responseActions = responseActions;
    }

    public void AddResponse(string response) {
        responses.Add(response);
    }

    public void AddResponseAction(Action responseAction) {
        responseActions.Add(responseAction);
    }

    public void AddResponseAndAction(string response, Action responseAction) {
        responses.Add(response);
        responseActions.Add(responseAction);
    }
    #endregion

    #region Builder
    public class Builder {
        private string question;
        private List<string> responses = new List<string>();
        private List<Action> responseActions = new List<Action>();

        public Builder SetQuestion(string question) {
            this.question = question;
            return this;
        }

        public Builder AddResponse(string response, Action responseAction) {
            responses.Add(response);
            responseActions.Add(responseAction);
            return this;
        }

        public QuestionNode Build() {
            if (string.IsNullOrEmpty(question) || responses.Count == 0 || responses.Count != responseActions.Count) {
                throw new InvalidOperationException("Question and responses must be set.");
            }
            if (responses.Count != responseActions.Count) {
                throw new InvalidOperationException("Number of responses must match number of response actions.");
            }

            return new QuestionNode(question, responses, responseActions);
        }
    }
    #endregion
}
