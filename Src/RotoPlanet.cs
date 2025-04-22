using System.ComponentModel.Design.Serialization;
using System.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogameRotoplanet.Systems.Input;

namespace MonogameRotoplanet;

public class RotoPlanet : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Texture2D ballTexture;
    Vector2 ballPosition;
    float ballSpeed;
    GameState state;

    public RotoPlanet()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                            _graphics.PreferredBackBufferHeight / 2);
        ballSpeed = 500f;
        state = GameState.Gameplay;

        InputManager.Init(new InputConfig());

        base.Initialize(); 
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        ballTexture = Content.Load<Texture2D>("images/ball");

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
                             Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        InputManager.Update();

        // The time since Update was called last.
        float updatedBallSpeed = ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        if (InputManager.IsKeyDown(state, InputAction.MoveUp))
        {
            ballPosition.Y -= updatedBallSpeed;
        }
        
        if (InputManager.IsKeyDown(state, InputAction.MoveDown))
        {
            ballPosition.Y += updatedBallSpeed;
        }
        
        if (InputManager.IsKeyDown(state, InputAction.MoveLeft))
        {
            ballPosition.X -= updatedBallSpeed;
        }
        
        if (InputManager.IsKeyDown(state, InputAction.MoveRight))
        {
            ballPosition.X += updatedBallSpeed;
        }

        // Boundary code
        if (ballPosition.X > _graphics.PreferredBackBufferWidth - ballTexture.Width / 2)
        {
            ballPosition.X = _graphics.PreferredBackBufferWidth - ballTexture.Width / 2;
        }
        else if (ballPosition.X < ballTexture.Width / 2)
        {
            ballPosition.X = ballTexture.Width / 2;
        }

        if (ballPosition.Y > _graphics.PreferredBackBufferHeight - ballTexture.Height / 2)
        {
            ballPosition.Y = _graphics.PreferredBackBufferHeight - ballTexture.Height / 2;
        }
        else if (ballPosition.Y < ballTexture.Height / 2)
        {
            ballPosition.Y = ballTexture.Height / 2;
        }
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(
            ballTexture,
            ballPosition,
            null,
            Color.White,
            0f,
            new Vector2(ballTexture.Width / 2, ballTexture.Height / 2),
            Vector2.One,
            SpriteEffects.None,
            0f
        );
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
