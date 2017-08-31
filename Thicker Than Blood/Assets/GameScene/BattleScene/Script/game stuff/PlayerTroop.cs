using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTroop : BattleInteractable {

    
    public GameObject controlPanel;
    public Person person { get; set; }
    public Grid curGrid { get; set; }
    
    // Use this for initialization
    public void Start()
    {
    }
    public override void cameraFocusOn()
    {
        base.cameraFocusOn();
        if (BattleCentralControl.playerTurn)
        {
            controlPanel.SetActive(true);
            controlPanel.GetComponent<TroopControlPanel>().curControledTroop = gameObject;
        }
    }
    
    public bool troopMoveToPlace(Grid grid)
    {
        if (!grid.occupied)
        {
            transform.LookAt(new Vector3(grid.x, 1, grid.z));
            transform.position = Vector3.Slerp(transform.position, new Vector3(grid.x, 1, grid.z), Time.deltaTime * 1f);
            /**while (gameObject.transform.position.x != grid.x ||
                gameObject.transform.position.z != grid.z)
            {
                
            }**/
        }
        if (transform.position == new Vector3(grid.x, 1, grid.z))
        {
            transform.LookAt(transform.position + new Vector3(0, 1, 0));
        }

        return true;
    }
    public void placed(Person personI, Grid curGridI)
    {
        personI.stamina = personI.getStaminaMax();
        person = personI;
        curGrid = curGridI;
        //person.stamina = person.getStaminaMax();
    }
    public List<GameObject> showMoveRange()
    {
        List<GameObject> result = new List<GameObject>();
        foreach(Grid g in BattleCentralControl.map)
        {
            g.mark = -1;
            g.path = new Queue<Grid>();
        }
        if (person.stamina > 0)
        {
            curGrid.mark = person.stamina;
            curGrid.path.Enqueue(curGrid);
            List<Grid> temp = new List<Grid>();
            temp.Add(curGrid);
            bfsHelper(curGrid);
        }
        foreach (Grid g in BattleCentralControl.map)
        {
            if (g.mark >= 0)
            {
                result.Add(BattleCentralControl.gridToObj[g]);
                BattleCentralControl.objsChanged.Add(BattleCentralControl.gridToObj[g]);
                BattleCentralControl.changeColor(BattleCentralControl.gridToObj[g]);
            }
        }
        
        return result;
    }
    public void bfsHelper(Grid grid)
    {
        //Debug.Log("curG nei: " + grid.neighbors.Count);
        foreach (Grid neiG in grid.neighbors)
        {
            //Debug.Log("curG: " + grid.mark);
            if (neiG.mark < grid.mark - neiG.staminaCost && grid.mark - neiG.staminaCost >= 0)
            {
                neiG.mark = grid.mark - neiG.staminaCost;
                neiG.path = new Queue<Grid>(grid.path);
                neiG.path.Enqueue(neiG);
                if (neiG.mark > 0)
                {
                    //Debug.Log("neigG: " + neiG.mark);
                    bfsHelper(neiG);
                }
            }
        }
    }
    
}

