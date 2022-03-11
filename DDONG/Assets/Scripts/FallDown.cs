using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown : MonoBehaviour
{
    #region Variables
    [SerializeField] private float fallingSpeed = 5;
    #endregion

    #region Unity Methods
    private void Update()
    {
        transform.Translate(Vector2.down * fallingSpeed * Time.deltaTime);
    }
    #endregion

    #region Helper Methods
    #endregion
}
