function PlayerInput({
    playerX,
    playerO,
    setPlayerX,
    setPlayerO,
    onStartGame

}) {
    const handleXChange = (event) => {
        setPlayerX(event.target.value);
    };

    const handleOChange = (event) => {
        setPlayerO(event.target.value);
    };

    const handleSubmit = () => {
        const trimedX = playerX.trim();
        const trimedO = playerO.trim();

        if (trimedX === '' || trimedO === '' ) {
            alert ('Ambos jugadores deben tener un nombre. ');
            return;
        }

        setPlayerX(trimedX);
        setPlayerO(trimedO);

        onStartGame();

    };

    return (
        <div className="modal-overlay">
            <div className="modal-content">
                <h2>Nombres de los jugadores</h2>
                <div className="input-group">
                    <label>Jugador X (X):</label>
                    <input 
                        type="text" 
                        value={playerX} 
                        onChange={handleXChange} 
                        placeholder="Nombre para X"
                    />
                </div>
                <div className="input-group">
                    <label>Jugador O (O):</label>
                    <input 
                        type="text" 
                        value={playerO} 
                        onChange={handleOChange} 
                        placeholder="Nombre para O"
                    />
                </div>
                <button onClick={handleSubmit}>
                    Comenzar juego
                </button>
            </div>
        </div>
    );

}

export default PlayerInput;