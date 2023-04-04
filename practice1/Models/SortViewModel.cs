using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practice1.Models
{
    public class SortViewModel
    {
        public SortState NameSort { get; set; } // значение для сортировки по имени
        public SortState YearSort { get; set; }    // значение для сортировки по возрасту
        public SortState Current { get; set; }     // значение свойства, выбранного для сортировки
        public bool Up { get; set; }  // Сортировка по возрастанию или убыванию

        public SortViewModel(SortState sortOrder)
        {
            // значения по умолчанию
            NameSort = SortState.NameAsc;
            YearSort = SortState.YearAsc;
            Up = true;

            if (sortOrder == SortState.YearDesc || sortOrder == SortState.NameDesc)
                
            {
                Up = false;
            }

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    Current = NameSort = SortState.NameAsc;
                    break;
                case SortState.YearAsc:
                    Current = YearSort = SortState.YearDesc;
                    break;
                case SortState.YearDesc:
                    Current = YearSort = SortState.YearAsc;
                    break;
                default:
                    Current = NameSort = SortState.NameDesc;
                    break;
            }
        }
    }
}
