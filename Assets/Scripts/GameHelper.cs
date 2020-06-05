using UnityEngine;
using System.Collections;

public class GameHelper : MonoBehaviour
{


    public BirdsHelper SelectedBirdsHelper { get; set; }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cour = Camera.main.ScreenToWorldPoint(Input.mousePosition);



        if (Input.GetMouseButtonDown(0))
        {   ///Нажали кнопку мыши
            if (SelectedBirdsHelper == null)
            {
                Collider2D[] all = Physics2D.OverlapCircleAll((Vector2)cour, 0.1f);

                foreach (var item in all)
                {
                    if (item.GetComponent<BirdsHelper>())
                    {
                        SelectedBirdsHelper = item.GetComponent<BirdsHelper>();
                        break;
                    }
                }
            }
        }

        if (SelectedBirdsHelper != null)
        {

            SelectedBirdsHelper.transform.position = Vector3.MoveTowards(
                SelectedBirdsHelper.transform.position,
                new Vector2(cour.x, cour.y),
                Time.deltaTime * 10.0f);

            var dir = SelectedBirdsHelper.StartPosition - new Vector2(SelectedBirdsHelper.transform.position.x, SelectedBirdsHelper.transform.position.y);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            SelectedBirdsHelper.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (SelectedBirdsHelper != null)
            {
                SelectedBirdsHelper.GetComponent<Rigidbody2D>().isKinematic = false;
                SelectedBirdsHelper.GetComponent<Rigidbody2D>().AddForceAtPosition(
                   SelectedBirdsHelper.transform.right * Vector3.Distance(SelectedBirdsHelper.transform.position, SelectedBirdsHelper.StartPosition) * 300,
                    SelectedBirdsHelper.StartPosition
                    );

                ///Логика выстрела
                SelectedBirdsHelper = null;
            }
        }
    }
}
