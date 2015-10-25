using UnityEngine;

/// <summary>
/// Base script to be associated to the team objects.
/// Players must be added as subnodes of team.
/// </summary>
public class Team : MonoBehaviour
{
    /// <summary>
    /// Display name of the team.
    /// </summary>
    public string name;

    /// <summary>
    /// Determines if the given team is an enemy of this team.
    /// </summary>
    /// <param name="other">The team to check if it's an enemy of this team.</param>
    /// <returns>true if the given team is an enemy of this team, false otherwise.</returns>
    public bool IsEnemyOf(Team other)
    {
        // At the moment, don't allow allies (create a free-for-all)
        return this != other;
    }
}
