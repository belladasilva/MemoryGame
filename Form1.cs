using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MemoryGame
{
    public partial class Form1 : Form
    {
        private Game game;
        private Card firstCard;
        private Card secondCard;
        private PictureBox[] cardPictureBoxes;
        private int score;
        private int movements;

        public Form1()
        {
            InitializeComponent();
            game = new Game();
            StartGame();
        }

        private void StartGame()
        {
            cardPictureBoxes = Controls.OfType<PictureBox>().ToArray();
            List<Card> gameCards = game.GetCards();
            game.ShuffleCards();

            for (int index = 0; index < cardPictureBoxes.Length; index++)
            {
                int cardIndex = index / 2; 
                cardPictureBoxes[index].Tag = gameCards[cardIndex]; 
                UpdatePictureBoxImage(cardPictureBoxes[index], Properties.Resources.back); 
                cardPictureBoxes[index].Enabled = true;
                cardPictureBoxes[index].Click += PictureBox_Click;
            }
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            Card clickedCard = (Card)pictureBox.Tag;

            if (!clickedCard.Flipped && (firstCard == null || secondCard == null))
            {
                clickedCard.Flipped = true;

                UpdatePictureBoxImage(pictureBox, GetImageForCard(clickedCard.Value));

                if (firstCard == null)
                    firstCard = clickedCard;
                else if (secondCard == null)
                {
                    if (clickedCard.Value == firstCard.Value)
                        return;

                    secondCard = clickedCard;
                    if (!game.CheckMatch(firstCard, secondCard))
                    {
                        pictureBox.Enabled = false;
                        Task.Delay(1000).ContinueWith(_ =>
                        {
                            firstCard.Flipped = false;
                            clickedCard.Flipped = false;
                            UpdateCardsDisplay();
                            pictureBox.Enabled = true;
                            firstCard = null;
                            secondCard = null;
                        });
                    }
                    else
                    {
                        firstCard = null;
                        secondCard = null;
                        CheckGameEnd();
                    }
                }
                movements++;
                movementsLabel.Text = "Movements: " + movements.ToString();

                if (game.CheckFlipped())
                {
                    MessageBox.Show("Game over! Score:" + score);
                    game.Reset();
                    score = 0;
                    movements = 0;
                    movementsLabel.Text = "Movements: " + movements.ToString();
                    UpdateCardsDisplay();
                }
            }
        }


        private void UpdateCardsDisplay()
        {
            foreach (PictureBox pictureBox in cardPictureBoxes)
            {
                Card card = (Card)pictureBox.Tag;
                if (card.Flipped)
                {
                    if (pictureBox.InvokeRequired)
                    {
                        pictureBox.Invoke((MethodInvoker)(() => pictureBox.Image = GetImageForCard(card.Value)));
                    }
                    else
                    {
                        pictureBox.Image = GetImageForCard(card.Value);
                    }
                }
                else
                {
                    if (pictureBox.InvokeRequired)
                    {
                        pictureBox.Invoke((MethodInvoker)(() => pictureBox.Image = Properties.Resources.back));
                    }
                    else
                    {
                        pictureBox.Image = Properties.Resources.back;
                    }
                }

                if (pictureBox.InvokeRequired)
                {
                    pictureBox.Invoke((MethodInvoker)(() => pictureBox.Enabled = !card.Flipped));
                }
                else
                {
                    pictureBox.Enabled = !card.Flipped;
                }
            }
        }

        private Image GetImageForCard(int cardValue)
        {
            switch (cardValue)
            {
                case 1:
                    return Properties.Resources.shrek;
                case 2:
                    return Properties.Resources.fiona;
                case 3:
                    return Properties.Resources.donkey;
                case 4: 
                    return Properties.Resources.puss;
                case 5: 
                    return Properties.Resources.lord;
                case 6:
                    return Properties.Resources.gingy;
                case 7: 
                    return Properties.Resources.pinocchio;
                case 8:
                    return Properties.Resources.baby;
                default:
                    return Properties.Resources.doris;
            }
        }

        private void UpdatePictureBoxImage(PictureBox pictureBox, Image image)
        {
            if (pictureBox.InvokeRequired)
            {
                pictureBox.Invoke((MethodInvoker)(() => UpdatePictureBoxImage(pictureBox, image)));
            }
            else
            {
                pictureBox.Image = image;
            }
        }
        private void CheckGameEnd()
        {
            if (game.CheckFlipped())
            {
                MessageBox.Show("Congratulations! You've won the game!");
                game.Reset();
                StartGame();
            }
        }

        private void leaveGame_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to leave the game?", "Leave Game", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                MessageBox.Show("Game over! Score: " + score);
                game.Reset();
                score = 0;
                movements = 0;
                movementsLabel.Text = "Movements: " + movements.ToString();
                UpdateCardsDisplay();
            }
        }
    }
}
