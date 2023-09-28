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
