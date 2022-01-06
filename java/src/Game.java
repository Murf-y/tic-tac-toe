import java.awt.*;
import java.awt.event.*;
import java.util.*;
import javax.swing.*;

public class Game implements ActionListener{
    Random random = new Random();
    JFrame jFrame = new JFrame();
    JPanel title_panel = new JPanel();
    JPanel button_panel = new JPanel();
    JPanel playAgain_panel = new JPanel();
    JLabel playAgain_field = new JLabel();
    JLabel text_field = new JLabel();
    JButton[] buttons = new JButton[9];

    boolean game_over;
    boolean player1_turn;
    Game(){
        jFrame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        jFrame.setSize(800,800);
        jFrame.getContentPane().setBackground(new Color(50,50,50));
        jFrame.setLayout(new BorderLayout());
        jFrame.setVisible(true);

        text_field.setBackground(new Color(25,25,25));
        text_field.setForeground(new Color(30,66,66));
        text_field.setFont(new Font("Ink Free", Font.BOLD, 75));
        text_field.setHorizontalAlignment(JLabel.CENTER);
        text_field.setText("Tic Tac Toe");
        text_field.setOpaque(true);

        button_panel.setLayout(new GridLayout(3,3));
        button_panel.setBackground(new Color(150,150,150));
        button_panel.setBounds(0,100,800,700);
        for(int i=0; i<9; i++){
            buttons[i]= new JButton();
            buttons[i].setFont(new Font("MV Boli",Font.BOLD,120));
            buttons[i].setBackground(new Color(40,40,40));
            buttons[i].setFocusable(false);
            buttons[i].addActionListener(this);
            button_panel.add(buttons[i]);
        }
        jFrame.add(button_panel);

        title_panel.setLayout(new BorderLayout());
        title_panel.setBounds(0,0,800,100);
        title_panel.add(text_field);
        jFrame.add(title_panel, BorderLayout.NORTH);
        firstTurn();
        game_over = false;
    }
    @Override
    public void actionPerformed(ActionEvent e) {
        if(game_over){return;}
        Arrays.stream(buttons).forEach(jButton -> {
            if (jButton == e.getSource()){
                if(jButton.getText()=="") {
                    jButton.setForeground(new Color(130, 60, 50));
                    if (player1_turn) {
                        jButton.setText("X");
                        player1_turn = false;
                        text_field.setText("O turn");
                    } else {
                        jButton.setText("O");
                        player1_turn = true;
                        text_field.setText("X turn");
                    }
                    check();
                }
            }
        });
    }
    public void showPlayAgain(){
        playAgain_field.setForeground(new Color(100,130,150));
        playAgain_field.setFont(new Font("Ink Free", Font.BOLD, 75));
        playAgain_field.setHorizontalAlignment(JLabel.CENTER);
        playAgain_field.setText("Play Again? Press R");
        playAgain_field.setOpaque(true);
        playAgain_field.setBackground(new Color(22,22,22));
        playAgain_panel.setLayout(new BorderLayout());
        playAgain_panel.setBounds(400,400,500,200);
        playAgain_panel.add(playAgain_field);
        jFrame.remove(button_panel);
        jFrame.add(playAgain_panel, BorderLayout.CENTER);
        jFrame.addKeyListener(new KeyListener() {
            @Override
            public void keyTyped(KeyEvent e) {
            }

            @Override
            public void keyPressed(KeyEvent e) {
                if(e.getKeyChar() == 'R' || e.getKeyChar() == 'r'){
                    jFrame.repaint(0,0,800,800);
                    jFrame.add(button_panel);
                    jFrame.remove(playAgain_panel);
                    game_over = false;
                    Arrays.stream(buttons).forEach(jButton -> {
                        jButton.setText("");
                    });
                }
            }

            @Override
            public void keyReleased(KeyEvent e) {
            }
        });

    }
    public void firstTurn(){
        try{
            Thread.sleep(2000);
        }
        catch (InterruptedException e){
            e.printStackTrace();
        }
        int randomInt = random.nextInt(2);
        if(randomInt == 0){
            player1_turn = true;
            text_field.setText("X turn");
        }
        else{
            player1_turn = false;
            text_field.setText("O turn");
        }
    }
    public void check(){
        int[][] lines = {
                {0,1,2},
                {3,4,5},
                {6,7,8},
                {0,3,6},
                {1,4,7},
                {2,5,8},
                {0,4,8},
                {2,4,6}
        };
        Arrays.stream(lines).forEach(line -> {
            if(!Objects.equals(buttons[line[0]].getText(), "")
                    && Objects.equals(buttons[line[0]].getText(), buttons[line[1]].getText())
                    && Objects.equals(buttons[line[0]].getText(), buttons[line[2]].getText())){
                game_over = true;
                if(!player1_turn){
                    xWins(line[0], line[1], line[2]);
                }
                else{
                    oWins(line[0],line[1],line[2]);
                }
            }
        });
    }
    public void xWins(int a, int b,int c){
        text_field.setText("X won");
        buttons[a].setForeground(new Color(50,200,50));
        buttons[b].setForeground(new Color(50,200,50));
        buttons[c].setForeground(new Color(50,200,50));
        showPlayAgain();
    }
    public void oWins(int a, int b,int c){
        text_field.setText("O won");
        buttons[a].setForeground(new Color(50,200,50));
        buttons[b].setForeground(new Color(50,200,50));
        buttons[c].setForeground(new Color(50,200,50));
        showPlayAgain();
    }
}
