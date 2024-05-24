using System;

public class ConditionNode : BaseDialogueNode {
    private Func<bool> conditionStatement;
    private BaseDialogueNode trueNode;
    private BaseDialogueNode falseNode;

    public ConditionNode() { }
    public ConditionNode(Func<bool> conditionStatement, BaseDialogueNode trueNode, BaseDialogueNode falseNode) {
        this.conditionStatement = conditionStatement;
        this.trueNode = trueNode;
        this.falseNode = falseNode;
    }

    public override void Evaluate() {
        if (conditionStatement()) {
            trueNode.Evaluate();
        } else {
            falseNode.Evaluate();
        }
    }

    public (BaseDialogueNode, BaseDialogueNode) GetChildren() {
        return (trueNode, falseNode);
    }

    #region Getters and Setters
    public void SetCondition(Func<bool> conditionStatement) {
        this.conditionStatement = conditionStatement;
    }

    public void SetTrueNode(BaseDialogueNode trueNode) {
        this.trueNode = trueNode;
    }

    public void SetFalseNode(BaseDialogueNode falseNode) {
        this.falseNode = falseNode;
    }
    #endregion

    #region Builder
    public class Builder {
        private Func<bool> conditionStatement;
        private BaseDialogueNode trueNode;
        private BaseDialogueNode falseNode;

        public Builder SetCondition(Func<bool> condition) {
            conditionStatement = condition;
            return this;
        }

        public Builder SetTrueNode(BaseDialogueNode node) {
            trueNode = node;
            return this;
        }

        public Builder SetFalseNode(BaseDialogueNode node) {
            falseNode = node;
            return this;
        }

        public ConditionNode Build() {
            if (conditionStatement == null || trueNode == null || falseNode == null) {
                throw new InvalidOperationException("Missing required parameters.");
            }
            return new ConditionNode(conditionStatement, trueNode, falseNode);
        }
    }
    #endregion
}
