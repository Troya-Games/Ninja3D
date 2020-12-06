using Miscs;


namespace PlayerState
{
    public class IdleState : IState
    { 
        readonly UIManager.StateUISettings _uıStateUISettings;

        IdleState(UIManager.StateUISettings _uı)
        {
            _uıStateUISettings = _uı;
        }
        
        public void EnterState()
        {
            _uıStateUISettings._gamePreUI.SetActive(true);
            _uıStateUISettings._gameInUI.SetActive(true);
        }

        public void ExitState()
        {
            _uıStateUISettings._gamePreUI.SetActive(false);
        }

   

        public void FixedUpdate()
        {
        }

        public void Update()
        {
         
        }
        
    }
    
   

}
