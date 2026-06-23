import { useTriquiGame } from '../Hooks/useTriquiGame';
import Square from './Square';

function Board({playerX, playerO}) {
    
    const {squares, winner, isDraw, xIsNext, handleClick, resetGame } = useTriquiGame();

    return (
        <div className="board">
            <h1>Triqui (Tres en linea)</h1>
            {winner && <h2>GAnador: {winner === 'X'? playerX: playerO} </h2>}
            {!winner && isDraw && <h2>Empate!</h2>}
            {!winner && !isDraw && <h2>Turno de: {xIsNext ? 'X' : 'O'}</h2>}
            <div className="grid">
                {squares.map((square, index) => (
                    <Square
                        key={index}
                        value = {square}
                        onSquareClick = { () => handleClick(index)}
                    />
                ))}
            </div>
            <button onClick={resetGame}>
                Reiniciar juego
            </button>
        </div>
    );
}

export default Board;