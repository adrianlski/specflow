using TechTalk.SpecFlow;

namespace GameCore.Specs
{
    [Binding]
    public class CommPlayerCharacterSteps
    {
        private readonly PlayerCharacterStepsContext _context;

        public CommPlayerCharacterSteps(PlayerCharacterStepsContext context)
        {
            _context = context;
        }


        [Given(@"I'm a new player")]
        public void GivenImANewPlayer()
        {
            _context.PlayerCharacter = new PlayerCharacter();
        }
    }
}