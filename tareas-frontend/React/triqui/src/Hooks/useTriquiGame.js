import {useState} from 'react';

    function calculateWinner(squares) {
        const lines = [
            [0, 1, 2,], [3, 4, 5], [6, 7, 8,],
            [0, 3, 6], [1, 4, 7], [2, 5, 8],
            [0, 4, 8,], [2, 4, 6]
        ];

        for (let line of lines) {
            const [a, b, c] = line;
            if (squares[a] && squares[a] === squares[b] && squares[a] === squares[c]) {
                return squares[a];
            }
        }
        return null
    }

export function useTriquiGame(){

    const [squares, setSquares] = useState(Array(9).fill(null));
    const [xIsNext, setXIsNext] = useState(true);


    const winner = calculateWinner(squares);
    const isDraw = !winner && squares.every(square => square !== null);

    const handleClick = (index) => {

        if (winner || squares[index] !== null) return;

        const newSquares = [...squares];

        newSquares[index] = xIsNext ? 'X' : 'O';

        setSquares(newSquares);

        setXIsNext(!xIsNext);

    };

    const resetGame = () => {
        setSquares(Array(9).fill(null));
        setXIsNext(true);
    }

    return {
        squares, 
        winner, 
        isDraw,
        xIsNext,
        handleClick,
        resetGame,
    }
}