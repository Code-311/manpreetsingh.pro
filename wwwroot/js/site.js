(() => {
  const revealItems = document.querySelectorAll('.section-reveal');

  const observer = new IntersectionObserver((entries) => {
    entries.forEach((entry) => {
      if (entry.isIntersecting) {
        entry.target.classList.add('is-visible');
        observer.unobserve(entry.target);
      }
    });
  }, { threshold: 0.2, rootMargin: '0px 0px -40px 0px' });

  revealItems.forEach((item) => observer.observe(item));

  const headerLinks = document.querySelectorAll('.header-nav a');
  headerLinks.forEach((link) => {
    link.addEventListener('click', () => {
      document.activeElement?.blur();
    });
  });
})();
