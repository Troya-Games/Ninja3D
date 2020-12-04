using Miscs;


namespace PlayerState
{
    public class IdleState : IState
    { 
        readonly UIManager.Settings _UISettings;

        IdleState(UIManager.Settings _uı)
        {
            _UISettings = _uı;
        }
        
        public void EnterState()
        {
            _UISettings._gamePreUI.SetActive(true);
          
            ResetMeshSlicer();
        }

        public void ExitState()
        {
        }

   

        public void FixedUpdate()
        {
        }

        public void Update()
        {
         
        }
        private void ResetMeshSlicer()
        {
            MeshSlicer.totalHit = 0;
            MeshSlicer.totalGameObjects.Clear();
        }
    }
    
   

}
