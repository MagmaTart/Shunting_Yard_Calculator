namespace Shunting_Yard_Algorithm
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.formulaText = new System.Windows.Forms.Label();
            this.answerText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // formulaText
            // 
            this.formulaText.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.formulaText.Font = new System.Drawing.Font("D2Coding", 35F);
            this.formulaText.Location = new System.Drawing.Point(4, 9);
            this.formulaText.Name = "formulaText";
            this.formulaText.Size = new System.Drawing.Size(1085, 68);
            this.formulaText.TabIndex = 4;
            this.formulaText.Text = "s";
            //this.formulaText.Click += new System.EventHandler(this.formulaText_Click);
            // 
            // answerText
            // 
            this.answerText.Font = new System.Drawing.Font("D2Coding", 25F);
            this.answerText.Location = new System.Drawing.Point(4, 121);
            this.answerText.Name = "answerText";
            this.answerText.Size = new System.Drawing.Size(1085, 68);
            this.answerText.TabIndex = 3;
            this.answerText.Text = "Result";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 401);
            this.Controls.Add(this.formulaText);
            this.Controls.Add(this.answerText);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label formulaText;
        public System.Windows.Forms.Label answerText;
    }
}

