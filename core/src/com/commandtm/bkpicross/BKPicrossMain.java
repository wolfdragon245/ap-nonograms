package com.commandtm.bkpicross;

import com.badlogic.gdx.ApplicationAdapter;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.math.Vector2;
import com.badlogic.gdx.utils.ScreenUtils;
import com.badlogic.gdx.utils.TimeUtils;

import java.util.ArrayList;

public class BKPicrossMain extends ApplicationAdapter {
	SpriteBatch batch;
	Texture fill;
	Texture empty;
	Texture nine;
	Texture eight;
	Texture seven;
	Texture six;
	Texture five;
	Texture four;
	Texture three;
	Texture two;
	Texture one;
	Texture zero;
	Vector2 touchPos;
	int clickY;
	int clickX;
	int row;
	long lastClick;
	
	@Override
	public void create () {
		batch = new SpriteBatch();
		Board localBoard = new Board();
		touchPos = new Vector2();
		fill  = new Texture("filled.png");
		empty = new Texture("empty.png");
		zero = new Texture("Numbers/0.png");
		one = new Texture("Numbers/1.png");
		two = new Texture("Numbers/2.png");
		three = new Texture("Numbers/3.png");
		four = new Texture("Numbers/4.png");
		five = new Texture("Numbers/5.png");
		six = new Texture("Numbers/6.png");
		seven = new Texture("Numbers/7.png");
		eight = new Texture("Numbers/8.png");
		nine = new Texture("Numbers/9.png");
	}

	@Override
	public void render () {
		ScreenUtils.clear(1, 1, 1, 1);
		batch.begin();
		for (int k = 0; k < Board.board.length; k++){
			for (int i = 0; i < Board.board[k].length; i++){
				batch.draw((Board.board[k][i]) ? fill : empty, ((32*i)+160), (320-(32*k)));
			}
		}
		verticalNums();
		horizontalNums();
		batch.end();
		if (Gdx.input.isTouched()){
			touchPos.set(Gdx.input.getX(), Gdx.input.getY());
			clickY = (int) ((touchPos.y-160)/32);
			clickX = (int) ((touchPos.x-160)/32);
			if (TimeUtils.nanoTime() - lastClick > 125000000){
				if (clickY >= 0 && clickY <= 9 && clickX >= 0 && clickX <= 9){
					Board.board[clickY][clickX] = !Board.board[clickY][clickX];
					lastClick = TimeUtils.nanoTime();
				}
			}
			System.out.println("Click!");
        }
	}
	
	@Override
	public void dispose () {
		batch.dispose();
	}

	/**
	 * Writes a given number at teh given cords
	 * @param num Number to write
	 * @param x X Cord of where to write
	 * @param y Y cord of where to write
	 */
	public void writeNums(int num, float x, float y){
        switch (num) {
            case 1 -> batch.draw(one, x, y);
            case 2 -> batch.draw(two, x, y);
            case 3 -> batch.draw(three, x, y);
            case 4 -> batch.draw(four, x, y);
            case 5 -> batch.draw(five, x, y);
            case 6 -> batch.draw(six, x, y);
            case 7 -> batch.draw(seven, x, y);
            case 8 -> batch.draw(eight, x, y);
            case 9 -> batch.draw(nine, x, y);
            case 10 -> {
                batch.draw(zero, x, y);
                batch.draw(one, x - 16, y);
            }
            default -> batch.draw(zero, x, y);
        }
	}

	/**
	 * Checks the puzzle solution and applies numbers on the vertical axis for the puzzle
	 */
	public void verticalNums(){
		row = 0;
		ArrayList<Integer> hints = new ArrayList<>();
		for (int k = (Board.boardSolution[Board.boardSolution[9].length-1].length-1); k >= 0; k--){
			for (int i = (Board.boardSolution.length-1); i >= 0; i--){
				if (Board.boardSolution[k][i]){
					row++;
				} else if (row > 0){
					hints.add(row);
					row = 0;
				}
				if (i == 0 && Board.boardSolution[k][i]){
					hints.add(row);
					row = 0;
				}
				if (i == 0 && hints.isEmpty()){
					hints.add(0);
				}
			}
			for (int i = 0; i < hints.size(); i++) {
				writeNums(hints.get(i), (128-(32*i)), (320-(32*k)));
			}
			hints.clear();
		}
	}

	public void horizontalNums(){
		row = 0;
		ArrayList<Integer> hints = new ArrayList<>();
		for (int k = (Board.boardSolution.length-1); k >= 0; k--){
			for (int i = (Board.boardSolution[k].length-1); i >= 0; i--){
				if (Board.boardSolution[k][i]){
					row++;
				} else if (row > 0){
					hints.add(row);
					row = 0;
				}
				if (i == 0 && Board.boardSolution[k][i]){
					hints.add(row);
					row = 0;
				}
				if (i == 0 && hints.isEmpty()){
					hints.add(0);
				}
			}
			for (int i = 0; i < hints.size(); i++) {
				writeNums(hints.get(i), (160+(32*k)), (320+(32*i)));
			}
			hints.clear();
		}
	}
}
