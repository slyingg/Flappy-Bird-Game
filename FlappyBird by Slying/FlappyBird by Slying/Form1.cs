using System;
using System.Windows.Forms;

namespace Flappy_Bird_by_slying
{
    public partial class Form1 : Form
    {
        int pipeSpeed = 9;
        int gravity = 6;
        int score = 0;

        public Form1()
        {
            InitializeComponent();
            developerText.Text = "Flappy Bird game by Slying";
            developerText.Visible = true;

            // Subscribe the event handlers for game controls (key presses)
            gameTimer.Tick += gameTimerEvent;
            KeyDown += gamekeyisdown;
            KeyUp += gamekeyisup;

            // Create a restart button
            Button restartButton = new Button();
            restartButton.Text = "Restart";
            restartButton.Size = new System.Drawing.Size(80, 40);
            restartButton.Location = new System.Drawing.Point(360, 200);
            restartButton.Visible = false; // Initially, hide the restart button
            Controls.Add(restartButton);

            restartButton.Click += (sender, e) => RestartGame();

        }

        private void gamekeyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                gravity = -6;
            }
            else if (e.KeyCode == Keys.Space)
            {
                if (restartButton.Visible) // Check if the restart button is visible
                {
                    RestartGame();
                }
            }
        }

        private void gamekeyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                gravity = 6;
            }
        }

        private void endGame()
        {
            gameTimer.Stop();
            scoreText.Text = "Score: " + score + " Game over!!!";
            restartButton.Visible = true; // Show the restart button
        }

        private void RestartGame()
        {
            // Reset game state
            flappyBird.Top = 170;
            pipeTop.Left = 950;
            pipeBottom.Left = 800;
            score = 0;
            pipeSpeed = 6;
            scoreText.Text = "Score: 0";
            restartButton.Visible = false; // Hide the restart button
            gameTimer.Start();
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            flappyBird.Top += gravity;
            pipeBottom.Left -= pipeSpeed;
            pipeTop.Left -= pipeSpeed;
            scoreText.Text = "Score: " + score;

            if (pipeBottom.Left < -150)
            {
                pipeBottom.Left = 800;
                score++;
            }

            if (pipeTop.Left < -180)
            {
                pipeTop.Left = 950;
                score++;
            }

            if (flappyBird.Bounds.IntersectsWith(pipeBottom.Bounds) ||
                flappyBird.Bounds.IntersectsWith(pipeTop.Bounds) ||
                flappyBird.Bounds.IntersectsWith(ground.Bounds) || flappyBird.Top < 0)
            {
                endGame();
            }

            if (score > 6)
            {
                pipeSpeed = 12;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Code to exit the application
            Application.Exit();
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Code to restart the game
            RestartGame();
        }
    }
}
