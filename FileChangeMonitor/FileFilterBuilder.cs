using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileChangeMonitor {
    public class FileFilterBuilder {
        /// <summary>
        /// A list of the <see cref="FileFilterItem"/> instances that make up the filter.
        /// </summary>
        private readonly IList<FileFilterItem> items;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileFilterBuilder"/> class.
        /// </summary>
        public FileFilterBuilder()
        {
            this.items = new List<FileFilterItem>();
        }

        /// <summary>
        /// Add a filter.
        /// </summary>
        /// <param name="description">The filter description.</param>
        /// <param name="extensions">The extensions included in the filter.</param>
        /// <returns>The <see cref="FileFilterBuilder"/> instance.</returns>
        public FileFilterBuilder Add(string description, params string[] extensions)
        {
            this.items.Add(new FileFilterItem(description, extensions));
            return this;
        }

        /// <summary>
        /// Returns a string representation of the filter.
        /// </summary>
        /// <returns>A string representation of the filter.</returns>
        public override string ToString()
        {
            return string.Join("|", this.items.Select(x => x.ToString()));
        }

        /// <summary>
        /// Implicit conversion from <see cref="FileFilterBuilder"/> to string.
        /// </summary>
        /// <param name="builder">The <see cref="FileFilterBuilder"/> to convert.</param>
        /// <returns>A string representation of the filter.</returns>
        public static implicit operator string(FileFilterBuilder builder)
        {
            return builder.ToString();
        }

        private class FileFilterItem {
            /// <summary>
            /// The filter description.
            /// </summary>
            private readonly string description;

            /// <summary>
            /// The filter pattern.
            /// </summary>
            private readonly string pattern;

            /// <summary>
            /// Initializes a new instance of the <see cref="FileFilterItem"/> class.
            /// </summary>
            /// <param name="description">The filter description.</param>
            /// <param name="extensions">The extensions included in the filter.</param>
            public FileFilterItem(string description, string[] extensions)
            {
                if (description == null) throw new ArgumentNullException(nameof(description));
                if (description.Length == 0) throw new ArgumentException("Value cannot be an empty string.", nameof(description));
                if (extensions == null) throw new ArgumentNullException(nameof(extensions));
                if (extensions.Length == 0) throw new ArgumentException("At least one extension must be supplied.", nameof(extensions));

                this.description = description;
                this.pattern = String.Join(";", extensions.Select(x => Path.ChangeExtension("*", x)));
            }

            /// <summary>
            /// Returns a string representation of the filter item.
            /// </summary>
            /// <returns>A string representation of the filter item.</returns>
            public override string ToString()
            {
                return $"{this.description} ({this.pattern})|{this.pattern}";
            }
        }
    }
}