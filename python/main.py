from typing import Dict, List, Tuple
from typing_extensions import IntVar, runtime
import pygame
from pygame.constants import QUIT

# Colors
WHITE = (233,233,233)
LIGHT_BLUE = (150,150,200)
DARK_BLUE = (50,50, 150)
RED = (255, 0, 0)
GREEN = (0, 255, 0)
BLUE = (0, 0, 255)
YELLOW = (255, 255, 0)
WHITE = (255, 255, 255)
BLACK = (0, 0, 0)
PURPLE = (128, 0, 128)
ORANGE = (255, 165, 0)
GREY = (128, 128, 128)
TURQOISE = (64, 224, 208)
# Game State
WIDTH = 900
HEIGHT = 600



class Square:
    def __init__(self, x , y, w, h, state = ""):
        self.x     = x
        self.y     = y
        self.w     = w
        self.h     = h
        self.state = state
        
    def draw(self,window):
        if(self.state):
            pygame.draw.rect(window, Board.pressedColor, (self.x, self.y, self.w, self.h))
            self.text = Text(self.state,self.x, self.y ,self.w, self.h, size = 150)
            self.text.draw(window)
        else:
            pygame.draw.rect(window, Board.unPressedColor, (self.x, self.y, self.w, self.h))
class Board:
    unPressedColor = LIGHT_BLUE
    pressedColor = PURPLE
    is_X_turn : bool = True
    is_game_over : bool = False
    def __init__(self,size = 3, width = 600, height = 600):
        self.size  : IntVar                     = size
        self.width : IntVar                     = width
        self.height : IntVar                    = height
        self.squares : List[IntVar]=  []
    def is_draw(self):
        for square in self.squares:
            if not square.state:
                return False
        return True
    def reset(self):
        self.squares = []
        self.create_squares()
        Board.is_game_over = False
        Board.is_X_turn = True
    def check_win(self):
        # check if the game is over
        lines = [
            [0,1,2],
            [3,4,5],
            [6,7,8],
            [0,3,6],
            [1,4,7],
            [2,5,8],
            [0,4,8],
            [2,4,6]   
        ]
        for line in lines:
            if (self.squares[line[0]].state != "" and self.squares[line[1]].state != "" and self.squares[line[2]].state != "" ):
                if (self.squares[line[0]].state == self.squares[line[1]].state and self.squares[line[1]].state == self.squares[line[2]].state):
                    Board.is_game_over = True
                    return True
        return False


    def create_squares(self):
        for row in range(self.size):
            for column in range(self.size):
                square_width = self.width // self.size 
                square_height = self.height // self.size 
                square = Square(row * square_width   , column * square_height , square_width, square_height)
                self.squares.append(square)
    def draw_borders(self,window):
        pygame.draw.rect(window, BLACK, (0,0, self.width, self.height), 5)
    def draw(self,window):
        window.fill(WHITE)
        for square in self.squares:
            square.draw(window)
        self.draw_lines(window)
        self.draw_borders(window)
    def draw_lines(self,window):
        for row in range(self.size):
            pygame.draw.line(window, BLACK, (0, row * (self.height // self.size) ), (self.width, row * (self.height // self.size) ))
        for column in range(self.size):
            pygame.draw.line(window, BLACK, (column * (self.width // self.size) , 0), (column * (self.width // self.size), self.height))
class Text:
    def __init__(self, text, x, y,w, h ,size = 50):
        self.text = text
        self.x = x
        self.y = y
        self.w = w
        self.h = h
        self.size = size
    def draw(self, window):
        font = pygame.font.SysFont("comicsans", self.size)
        text = font.render(self.text, 1, BLACK)
        window.blit(text, (self.x + (self.w / 2 - text.get_width() / 2), self.y + (self.h / 2 - text.get_height() / 2)))
class Text_Box:
    def __init__(self, x, y, w, h, text = "", size = 50):
        self.x = x
        self.y = y
        self.w = w
        self.h = h
        self.text = text
        self.size = size
    def draw(self, window):
        pygame.draw.rect(window, BLACK, (self.x, self.y, self.w, self.h), 1)
        font = pygame.font.SysFont("comicsans", self.size)
        text = font.render(self.text, 1, BLACK)
        window.blit(text, (self.x + (self.w / 2 - text.get_width() / 2), self.y + (self.h / 2 - text.get_height() / 2)))
    def update(self, text):
        self.text = text
class Button:
    def __init__(self, x, y, w, h, text = "", size = 50):
        self.x = x
        self.y = y
        self.w = w
        self.h = h
        self.text = text
        self.size = size
    def draw(self, window):
        pygame.draw.rect(window, DARK_BLUE, (self.x, self.y, self.w, self.h))
        font = pygame.font.SysFont("comicsans", self.size)
        text = font.render(self.text, 1, BLACK)
        window.blit(text, (self.x + (self.w / 2 - text.get_width() / 2), self.y + (self.h / 2 - text.get_height() / 2)))
    def update(self, text):
        self.text = text
    def is_clicked(self, mouse):
        if mouse[0] > self.x and mouse[0] < self.x + self.w:
            if mouse[1] > self.y and mouse[1] < self.y + self.h:
                return True
        return False

def main():
    pygame.init()
    pygame.font.init()
    pygame.display.set_caption("Tic Tac Toe")
    window = pygame.display.set_mode((WIDTH, HEIGHT))

    board= Board()
    board.create_squares()
    turn_text = Text_Box(WIDTH / 2 + 200, HEIGHT / 2 - 100, 200, 200, "X's Turn")
    reset_button = Button(WIDTH / 2 + 200, HEIGHT / 2 + 150, 200, 50, "Reset")
    running = True 
    while running:
        board.draw(window)
        turn_text.draw(window)
        reset_button.draw(window)

        events = pygame.event.get()
        for event in events:
            if event.type == pygame.QUIT:
                running = False
            if event.type == pygame.MOUSEBUTTONDOWN:
                pos = pygame.mouse.get_pos()
                if reset_button.is_clicked(pos):
                    board.reset()
                    Board.is_X_turn = False
                    turn_text.update("X's Turn")
                else:
                    for square in board.squares:
                        if square.state == "":
                            if board.is_game_over == False:
                                if square.x < pos[0] < square.x + square.w and square.y < pos[1] < square.y + square.h:
                                    square.state = "X" if board.is_X_turn else "O"
                                    turn_text.update("O's Turn" if board.is_X_turn else "X's Turn")
                                    if board.check_win():
                                        turn_text.text = "X Wins" if board.is_X_turn else "O Wins"
                                        break
                                    if board.is_draw():
                                        turn_text.update("Draw")
                                        break
                                    board.is_X_turn = not board.is_X_turn
                                    break
        pygame.display.update()
        
    pygame.quit()

if __name__ == "__main__":
    main()