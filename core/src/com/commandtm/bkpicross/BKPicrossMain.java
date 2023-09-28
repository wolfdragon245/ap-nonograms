package com.commandtm.bkpicross;

import com.badlogic.gdx.ApplicationAdapter;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.math.Vector2;
import com.badlogic.gdx.utils.ScreenUtils;
import com.badlogic.gdx.utils.TimeUtils;

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
				batch.draw((Board.board[k][i]) ? fill : empty, ((32*i)+160), (480-(32*k)));
			}
		}
		batch.draw(zero, 128, 480);
		batch.draw(one, 112, 480);
		batch.draw(nine, 128, 448);
		batch.draw(eight, 128, 416);
		batch.draw(seven, 128, 384);
		batch.draw(six, 128, 352);
		batch.draw(five, 128, 320);
		batch.draw(four, 128, 288);
		batch.draw(three, 128, 256);
		batch.draw(two, 128, 224);
		batch.draw(one, 128, 192);
		batch.draw(zero, 128, 160);
		batch.end();
		if (Gdx.input.isTouched()){
			touchPos.set(Gdx.input.getX(), Gdx.input.getY());
			clickY = (int) ((touchPos.y)/32);
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
}
