using PuzzleSolver.Extenders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuzzleSolver.Forms
{
    /// <summary>
    /// Редактирование объекта
    /// </summary>
    public partial class ObjectForm : Form
    {
        /// <summary>
        /// Редактирумый объект
        /// </summary>
        private readonly object o;

        /// <summary>
        /// Беспараметрический конструктор
        /// </summary>
        public ObjectForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Конструктор по объекту
        /// </summary>
        /// <param name="o"></param>
        public ObjectForm(object o) : this()
        {
            this.o = o;
            Type type = o.GetType();
            int top = 10;
            foreach (PropertyInfo property in type.GetProperties().OrderBy(x => x.Order()))
            {
                // Отображаемое имя
                var attr = property.GetCustomAttribute<DisplayNameAttribute>();
                // Пропуск скрытых полей
                if (attr == null)
                {
                    continue;
                }

                Control control;
                switch (property.PropertyType.Name)
                {
                    case nameof(Boolean):
                        control = new CheckBox()
                        {
                            Text = attr.DisplayName,
                            Left = 10,
                            Top = top,
                            AutoSize = true,
                            Tag = property,
                            Checked = (bool)property.GetValue(o)
                        };
                        break;
                    case nameof(Int32):
                        var label = new Label()
                        {
                            Text = attr.DisplayName,
                            Left = 10,
                            Top = top,
                            AutoSize = true,
                        };

                        panel.Controls.Add(label);

                        control = new TextBox()
                        {
                            Left = panel.Width * 2 / 3,
                            Top = top,
                            Width = panel.Width / 4,
                            Tag = property,
                            Text = property.GetValue(o).ToString()
                        };
                        break;

                    default:
                        throw new Exception($"Тип данных {property.PropertyType.Name} не поддерживается");
                }

                panel.Controls.Add(control);

                top += control.Height + 10;
            }
        }

        /// <summary>
        /// Сохранение данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            foreach (Control control in panel.Controls)
            {
                if (control.Tag is PropertyInfo property)
                {
                    object value = control.Text;
                    switch (property.PropertyType.Name)
                    {
                        case nameof(Boolean):
                            value = ((CheckBox)control).Checked;
                            break;
                        case nameof(Int32):
                            value = int.Parse(control.Text);
                            break;
                        default:
                            throw new Exception($"Тип данных {property.PropertyType.Name} не поддерживается");
                    }

                    property.SetValue(o, value);
                }
            }
            DialogResult = DialogResult.OK;
        }
    }
}
