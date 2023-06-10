# MoveStopMove_Phongpkt
MoveIo_Game <br>
_Gameplay:
- Player:
    + Người chơi sử dụng Joystick để di chuyển
    + Bot hoặc Player bắt buộc phải dừng lại mới chuyển sang state Attack
    + Người chơi sẽ hiện tầm đánh
    + Khi bot vào tầm đánh của người chơi sẽ phải có vòng target dưới chân của bot
    + Các state của player: Idle, Run, Attack, Ulti, Die, Win
- Bot:
    + Các state của bot: Idle, Run, Attack, Ulti, Die
    + Bot không hiện tầm đánh
    + Bot sử dụng NavMesh Agent
- Weapon:
    + Có 2 loại weapon: bullet, weapon trên tay nhân vật
    + Khi nhân vật ném vũ khí -> sinh ra bullet bay và mất vũ khí trên tay nhân vật
- Level:
    + Trong game sẽ có điểm của nhân vật
    + Khi đến 1 mốc điểm nhất định, kích thước và tầm đánh của nhân vật sẽ được tăng lên
    + Trong level sẽ có các hộp quà để nhân vật sử dụng Ulti (kiểu đánh đặc biệt)
    + Kiểm soát số lượng bot ở trên map và số lượng bot ở trong game (để respawn)
    + Trong map sẽ có Obstacle (vật cản), khi nhân vật đến gần sẽ bị làm mờ đi
- Game:
    + Các game state: gamePlay, mainMenu, gameEnd, gameWin
    + Main menu sẽ hiện khi người chơi vào game
    + GameState sẽ chuyển sang gamePlay khi người chơi ấn Play
    + gameEnd nếu người chơi chết
    + gameWin nếu người chơi là người cuối cùng trên map
