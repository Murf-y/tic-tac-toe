class Square{
    static isXturn;
    static isRunning;
    constructor(element, checkGameOver){
        this.element = element;
        this.addClickListener();
        this.checkGameOver = checkGameOver;
    }
    Activate() {
        this.element.classList.add("active");
    }
    Deactivate(){
        this.element.classList.remove("active");
    }
    addClickListener(){
        this.element.addEventListener("click",
        (e) => {
            this.HandleClicked();
        })
    }

    HandleClicked(){
        console.log(Square.isRunning)
        if(!this.element.classList.contains("active")){return;}
        if(!Square.isRunning) {return;}
        this.Deactivate();
        this.element.style.backgroundColor =  "rgba(23, 50, 82, 0.9)";
        this.element.innerText = Square.isXturn ? "O" : "X";
        document.getElementsByClassName("turn")[0].innerText = Square.isXturn ? "X Turn" : "O Turn";
        Square.isXturn = !Square.isXturn;
        this.checkGameOver();
    }

}
function main() {
    var board = InitBoard();
    var isXStart = true;
    var isRunning = true;
    document.getElementsByClassName("reset")[0].addEventListener("click" ,()=> {Reset()})

    function  InitBoard() {
        let board = [];
        let square_elements = document.getElementsByClassName("square");
        for (let index = 0; index < square_elements.length; index++) {
            const element = square_elements[index];
            square = new Square(element, checkGameOver);
            board.push(square);
        }
        Square.isXturn = isXStart;
        Square.isRunning = true;
        return board;
    }
    function checkGameOver(){
        const lines = [
            [0,1,2],
            [3,4,5],
            [6,7,8],
            [0,3,6],
            [1,4,7],
            [2,5,8],
            [0,4,8],
            [2,4,6]
        ]
        lines.forEach(line => {
            a = board[line[0]].element.innerText;
            b = board[line[1]].element.innerText;
            c = board[line[2]].element.innerText;
            if(a != "" && a==b&& a==c){
                winnerText = document.getElementsByClassName("winner")[0];
                winnerText.innerText = Square.isXturn ?  "X Won!":"O won!";
                document.getElementsByClassName("turn")[0].innerText =" - - - ";
                isRunning = false;
                Square.isRunning = isRunning;
                board.forEach(square => {
                    square.Deactivate();
                    square.element.style.backgroundColor="rgba(23, 50, 82, 0.9)";
                });
                return;
            }

        });
    }
    function Reset() {
        board.forEach(square => {
            square.Activate();
            square.element.innerText = "";
            square.element.style.backgroundColor="rgba(34, 68, 107, 0.9)";
            Square.isRunning = true;
        });
        document.getElementsByClassName("turn")[0].innerText ="";
        document.getElementsByClassName("winner")[0].innerText ="";
    }
}

main()
