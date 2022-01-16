using UnityEngine;

[CreateAssetMenu(fileName = "NewConversation", menuName = "Conversation", order = 1)]
public class Texting : ScriptableObject
{
    public TextingEntry[] conversationLines;
}
