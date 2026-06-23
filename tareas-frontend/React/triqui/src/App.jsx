import { useState } from 'react';
import Board from "./Components/Board.jsx";
import PlayerInput from './Components/PLayerInput.jsx';

function App() {
  const [playerX, setPlayerX] = useState('Jugador X');
  const [playerO, setPlayerO] = useState('Jugador O');
  const [gameStarted, setGameStarted] = useState(false);

  return (
    <div className="App">
      <Board
      playerX = {playerX}
      playerO = {playerO}
      setPlayerX = {setPlayerX}
      setPlayerO = {setPlayerO}
      />
      {!gameStarted && (
        <PlayerInput
        playerX={playerX}
        playerO={playerO}
        setPlayerX={setPlayerX}
        setPlayerO={setPlayerO}
        onStartGame={ () => setGameStarted(true)}
        />
      )}
    </div>
  );
}

export default App;

